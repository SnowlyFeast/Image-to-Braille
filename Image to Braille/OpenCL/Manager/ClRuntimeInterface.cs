using System;

using OpenCL.Net;
using OpenCL.Components;

namespace OpenCL.Manager
{
    abstract class ClRuntimeInterface : IDisposable
    {
        [Flags]
        protected enum Status
        {
            Ready = 0x00,
            DeviceNotReady = 0x01,
            ContextNotReady = 0x02,
            CommandQueueNotReady = 0x04,
            KernelNotReady = 0x08,
            NotInitialized = 0x0F
        }
        public bool IsReady{
            get {
                if (status == Status.Ready)
                    return true;
                else
                    return false; }
        }

        protected Device device;
        protected Context context;
        protected CommandQueue commandQueue;

        protected Status status;
        protected ErrorCode error;
        
        
        public ClRuntimeInterface(ClDeviceIndex targetDeviceIndex, string programText)
        {
            status = Status.NotInitialized;

            SetupDevice(targetDeviceIndex); 
            SetupContext();
            SetupCommandQueue();
            SetupKernel(programText);
            IsErrorOccurred();
        }

        private void SetupDevice(ClDeviceIndex targetDeviceIndex)
        {
            int platformIndex = targetDeviceIndex.Platform;
            int deviceIndex = targetDeviceIndex.Device;

            //Get platform
            Platform targetPlatform;
            if (platformIndex == 0)
                targetPlatform = ClDeviceManager.GetPlatform(out error)[0];
            else
                targetPlatform = ClDeviceManager.GetPlatform(out error)[platformIndex - 1];

            //Check error
            if (IsErrorOccurred())
                return;

            //Get device
            if (deviceIndex == 0)
                device = ClDeviceManager.GetDefaultDevice(targetPlatform, out error);
            else
                device = ClDeviceManager.GetDevice(targetPlatform, out error)[deviceIndex - 1];

            //Device is ready
            if (!IsErrorOccurred())
                status ^= Status.DeviceNotReady;
        }
        private void SetupContext()
        {
            //Abort if device is not ready
            if ((status & Status.DeviceNotReady) == Status.DeviceNotReady)
                return;

            //Create context
            context = ClContextManager.CreateContext(device, out error);

            //Context is ready
            if (!IsErrorOccurred())
                status ^= Status.ContextNotReady;
        }
        private void SetupCommandQueue()
        {
            //Abort if context is not ready
            if ((status & Status.ContextNotReady) == Status.ContextNotReady)
                return;

            //Create Command Queue
            commandQueue = ClContextManager.CreateCommandQueue(context, device, out error);

            //Command Queue is ready
            if (!IsErrorOccurred())
                status ^= Status.CommandQueueNotReady;
        }
        private void SetupKernel(string programText)
        {
            //Abort if Command Queue is not ready
            if ((status & Status.CommandQueueNotReady) == Status.CommandQueueNotReady)
                return;

            //Load kernel
            using(Program program = ClContextManager.BuildProgram(context, device, programText, out error))
            {
                if (IsErrorOccurred())
                    return;

                AddKernel(program);
            }

            //Kernel is ready
            if (!IsErrorOccurred())
                status ^= Status.KernelNotReady;
        }

        protected bool IsErrorOccurred()
        {
            if ((status != Status.Ready) && (error != ErrorCode.Success))
            {
                NotifyError("Status : " + status.ToString() + ", Error : " + error.ToString()
                    + System.Environment.NewLine + System.Environment.StackTrace);
                return true;
            }
            return false;
        }

        abstract protected void AddKernel(Program program);
        abstract protected void NotifyError(String message);
        abstract protected void DisposeKernel();
        
        public void Dispose()
        {
            if ((status & Status.KernelNotReady) != Status.KernelNotReady)
            {
                DisposeKernel();
            }
            if((status & Status.CommandQueueNotReady) != Status.CommandQueueNotReady)
                commandQueue.Dispose();
            if((status & Status.ContextNotReady) != Status.ContextNotReady)
                context.Dispose();
        }
        
    }
}

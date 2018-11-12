using OpenCL.Net;
using System;

namespace OpenCL.Components
{
    abstract class ClKernel : IDisposable
    {
        protected enum ObjectType
        {
            Int,
            Double,
            ClMem
        }

        public bool IsReady { get { return initialized; } }
        protected Context Context { get { return context; } }
        protected CommandQueue CommandQueue { get { return commandQueue; } }


        private readonly Context context;
        private readonly CommandQueue commandQueue;
        private Net.Kernel kernel;

        private IntPtr[] globalWorkGroupSize;
        private IntPtr[] origin;

        private bool initialized = false;
        

        public ClKernel(Context context, CommandQueue commandQueue, OpenCL.Net.Program program, string KernelName, out ErrorCode error)
        {
            this.context = context;
            this.commandQueue = commandQueue;
            origin = new IntPtr[] { (IntPtr)0, (IntPtr)0, (IntPtr)0 };
            kernel = Cl.CreateKernel(program, KernelName, out error);

            if (error != ErrorCode.Success)
                initialized = false;
            else
                initialized = true;
        }

        public void ExecuteKernel(out ErrorCode error, params object[] args)
        {
            //Check initialized
            if (!initialized) {
                error = ErrorCode.InvalidKernel;
                return;
            }
            
            //Setup arguments
            Setup(out error, args);

            if (error != ErrorCode.Success) //Check error
                return;

            //Run kernel
            error = Cl.EnqueueNDRangeKernel(commandQueue, kernel, 2, null, globalWorkGroupSize, null, 0, null, out Event clEvent);
            error |= Cl.Finish(commandQueue);

            if (error != ErrorCode.Success) //Check error
                return;

            //Collect result
            OnPostExecute(out error, args);
        }

        /*
         *  Kernel's unique actions
         * 
         * 'Setup' for set kernel argumnets,
         * 'OnPostExecute' for collect kernel operation result.
         */
        abstract protected void Setup(out ErrorCode error, params object[] args);
        abstract protected void OnPostExecute(out ErrorCode error, params object[] args);


        //Internal supporting method
        protected void SetKernelArg(object target, uint index, ObjectType objectType, out ErrorCode error)
        {
            switch (objectType)
            {
                default:
                case ObjectType.Int:
                    error = Cl.SetKernelArg(kernel, index, (IntPtr)sizeof(int), target);
                    break;

                case ObjectType.Double:
                    error = Cl.SetKernelArg(kernel, index, (IntPtr)sizeof(double), target);
                    break;

                case ObjectType.ClMem:
                    error = Cl.SetKernelArg(kernel, index, (IMem)target);
                    break;
            }
        }

        protected void EnqueueReadImage(IMem targetMemory, byte[] destination, out ErrorCode error)
        {
            error = Cl.EnqueueReadImage(commandQueue, targetMemory, Bool.True, origin, 
                globalWorkGroupSize, (IntPtr)0, (IntPtr)0, destination, 0, null, out Event clEvent);
        }

        protected void EnqueueReadBuffer<T>(IMem targetMemory, T[] destination, out ErrorCode error) where T : struct
        {
            error = Cl.EnqueueReadBuffer(commandQueue, targetMemory, Bool.True, 0, 
                destination.Length, destination, 0, null, out Event clEvent);
        }
        
        protected void SetGlobalWorkGroupSize(int width, int height)
        {
            globalWorkGroupSize = new IntPtr[] { (IntPtr)width, (IntPtr)height, (IntPtr)1 };
        }
        

        public void Dispose()
        {
            if (initialized)
                kernel.Dispose();

            initialized = false;
        }
    }
}

using System;
using System.Drawing;
using OpenCL.Net;

namespace OpenCL.Components.Kernel
{
    sealed class KernelGrayscale : ClKernel, IDisposable
    {
        private const string KERNEL_NAME = "Grayscale";
        

        public KernelGrayscale(Context context, CommandQueue commandQueue, OpenCL.Net.Program program, out ErrorCode error) 
            : base(context, commandQueue, program, KERNEL_NAME, out error)
        {
            
        }
        
        protected override void Setup(out ErrorCode error, params object[] args)
        {
            int width = ((ClImage2D)args[1]).Width;
            int height = ((ClImage2D)args[1]).Height;

            SetGlobalWorkGroupSize(width, height);
            SetKernelArg(((ClImage2D)args[0]).GetClMem, 0, ObjectType.ClMem, out error);
            SetKernelArg(((ClImage2D)args[1]).GetClMem, 1, ObjectType.ClMem, out error);
        }

        protected override void OnPostExecute(out ErrorCode error, params object[] args)
        {
            ClImage2D destination = (ClImage2D)args[1];
            byte[] outputData = new byte[destination.Size];
            EnqueueReadImage(destination.GetClMem, outputData, out error);
            if (error != ErrorCode.Success)
                return;

            destination.WriteToOriginalBitmap(outputData);
        }
    }
}

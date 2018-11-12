using System;
using System.Drawing;
using OpenCL.Net;

namespace OpenCL.Components.Kernel
{
    sealed class KernelGenerateUnicode : ClKernel, IDisposable
    {
        private const string KERNEL_NAME = "GenereateUnicode";
        

        public KernelGenerateUnicode(Context context, CommandQueue commandQueue, OpenCL.Net.Program program, out ErrorCode error) 
            : base(context, commandQueue, program, KERNEL_NAME, out error)
        {
            
        }
        
        protected override void Setup(out ErrorCode error, params object[] args)
        {
            int width = ((ClImage2D)args[0]).Width / 2;
            int height = ((ClImage2D)args[0]).Height / 4;

            SetGlobalWorkGroupSize(width, height);
            SetKernelArg(((ClImage2D)args[0]).GetClMem, 0, ObjectType.ClMem, out error);
            SetKernelArg(((ClBuffer<short>)args[1]).GetClMem, 1, ObjectType.ClMem, out error);
        }

        protected override void OnPostExecute(out ErrorCode error, params object[] args)
        {
            ClBuffer<short> destination = (ClBuffer<short>)args[1];
            EnqueueReadBuffer(destination.GetClMem, destination.GetBuffer(), out error);
        }
    }
}

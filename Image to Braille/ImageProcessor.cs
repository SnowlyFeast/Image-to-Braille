using System;
using System.Drawing;
using System.Runtime.InteropServices;

using OpenCL.Net;
using OpenCL.Manager;
using OpenCL.Components;
using OpenCL.Components.Kernel;
using IOMode = OpenCL.Components.ClImage2D.IOMode;
using ChannelType = OpenCL.Components.ClImage2D.ChannelType;

namespace Image_to_Braille
{
    sealed class ImageProcessor : ClRuntimeInterface
    {
        private ClKernel kernelResize;
        private ClKernel kernelLaplacian;
        private ClKernel kernelSobel;
        private ClKernel kernelGrayscale;
        //private ClKernel kernelHybrid;
        private ClKernel kernelQuantize;
        private ClKernel kernelGenerateUnicode;
        private ClKernel kernelGammaCorrection;
        private ClKernel kernelContrastStretch;

        public ImageProcessor(ClDeviceIndex targetDeviceIndex, string programText) 
            : base(targetDeviceIndex, programText)
        {
        }

        protected override void AddKernel(OpenCL.Net.Program program)
        {
            kernelResize = new KernelResize(context, commandQueue, program, out error);
            kernelLaplacian = new KernelLaplacian(context, commandQueue, program, out error);
            kernelSobel = new KernelSobel(context, commandQueue, program, out error);
            kernelGrayscale = new KernelGrayscale(context, commandQueue, program, out error);
            //kernelHybrid = new KernelHybrid(context, commandQueue, program, out error);
            kernelQuantize = new KernelQuantize(context, commandQueue, program, out error);
            kernelGenerateUnicode = new KernelGenerateUnicode(context, commandQueue, program, out error);
            kernelGammaCorrection = new KernelGammaCorrection(context, commandQueue, program, out error);
            kernelContrastStretch = new KernelContrastStretch(context, commandQueue, program, out error);
        }

        protected override void NotifyError(string message)
        {
            Form1.SaveErrorLog(message);
        }


        public void ResizeImage(Bitmap source, Bitmap destination)
        {
            ImgToImgKernel(kernelResize, source, destination);
        }

        public void ApplyLaplacian(Bitmap source, Bitmap destination)
        {
            ImgToImgKernel(kernelLaplacian, source, destination);
        }

        public void ApplySobelMask(Bitmap source, Bitmap destination)
        {
            ImgToImgKernel(kernelSobel, source, destination);
        }

        public void ApplyGrayScaleMask(Bitmap source, Bitmap destination)
        {
            ImgToImgKernel(kernelGrayscale, source, destination);
        }
        

        public void QuantizeImage(Bitmap source, Bitmap destination, int threshold)
        {
            ImgToImgKernel(kernelQuantize, source, destination, threshold);
        }

        public String GenerateUnicode(Bitmap source, String spacingChar)
        {
            //Check status
            if (IsErrorOccurred())
                return String.Empty;

            //Create Cl memory
            ClImage2D srcMem = new ClImage2D(source, context, IOMode.ReadOnly, ChannelType.RGBA32bpp, out error);
            int totalUnicodeLength = (srcMem.Width / 2 + 2) * (srcMem.Height / 4);
            ClBuffer<short> dstMem = new ClBuffer<short>(context, totalUnicodeLength, out error);

            //Check error
            if (!srcMem.Ready || !dstMem.Ready || IsErrorOccurred())
            {
                srcMem.Dispose();
                dstMem.Dispose();
                return String.Empty;
            }

            //Run kernel
            kernelGenerateUnicode.ExecuteKernel(out error, new object[] { srcMem, dstMem });
            if (IsErrorOccurred())
                return String.Empty;

            //Marshaling unicode data.
            IntPtr stream = Marshal.AllocHGlobal(totalUnicodeLength * 2);
            Marshal.Copy(dstMem.GetBuffer(), 0, stream, totalUnicodeLength);
            string result = Marshal.PtrToStringUni(stream, totalUnicodeLength);

            //Replace spacing char
            result = result.Replace(((char)0x2800).ToString(), spacingChar);

            //Clean up
            srcMem.Dispose();
            dstMem.Dispose();

            return result;
        }
        
        private void ImgToImgKernel(ClKernel kernel, Bitmap source, Bitmap destination, params object[] extra)
        {
            //Check status
            if (error != ErrorCode.Success)
                return;

            //Create Cl memory
            ClImage2D srcMem = new ClImage2D(source, context, IOMode.ReadOnly, ChannelType.RGBA32bpp, out error);
            ClImage2D dstMem = new ClImage2D(destination, context, IOMode.WriteOnly, ChannelType.RGBA32bpp, out error);

            //Check error
            if (!srcMem.Ready || !dstMem.Ready || IsErrorOccurred())
            {
                srcMem.Dispose();
                dstMem.Dispose();
                if (error != ErrorCode.Success)
                    return;
            }

            //Create argument list
            object[] argList = new object[2 + extra.Length];
            argList[0] = srcMem;
            argList[1] = dstMem;
            extra.CopyTo(argList, 2);

            //Run kernel
            kernel.ExecuteKernel(out error, argList);
            if (error != ErrorCode.Success)
                return;

            //Clean up
            srcMem.Dispose();
            dstMem.Dispose();
        }

        protected override void DisposeKernel()
        {
            kernelResize.Dispose();
            kernelLaplacian.Dispose();
            kernelSobel.Dispose();
            kernelGrayscale.Dispose();
            kernelQuantize.Dispose();
            kernelGenerateUnicode.Dispose();
            kernelGammaCorrection.Dispose();
            kernelContrastStretch.Dispose();
        }

    }
}

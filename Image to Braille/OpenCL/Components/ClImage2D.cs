using OpenCL.Net;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace OpenCL.Components
{
    class ClImage2D : IDisposable
    {
        public enum IOMode
        {
            /// <summary>
            /// Create memory with R only mode.
            /// </summary>
            ReadOnly,
            /// <summary>
            /// Create memory with W only mode.
            /// </summary>
            WriteOnly,
            /// <summary>
            /// Create memory with RW mode. Not recommended.
            /// </summary>
            ReadWrite
        }
        public enum ChannelType
        {
            Single8bpp,
            RGBA32bpp
        }

        public int Width { get; }
        public int Height { get; }
        public int Stride { get; }
        public int Size { get; }

        public bool Ready
        {
            get
            {
                return ready;
            }
        }
        public IMem GetClMem
        {
            get
            {
                return clMem;
            }
        }


        private IOMode iOMode;
        private Bitmap original;
        private BitmapData bitmapData;
        private IMem clMem;
        private bool ready;

        private readonly OpenCL.Net.ImageFormat clImageFormat;
        private readonly PixelFormat pixelFormat;
        
        public ClImage2D(Bitmap bitmap, Context context, IOMode iOMode, ChannelType channelType, out ErrorCode error)
        {
            switch (channelType)
            {
                case ChannelType.RGBA32bpp:
                    clImageFormat = new OpenCL.Net.ImageFormat(ChannelOrder.RGBA, Net.ChannelType.Unsigned_Int8);
                    pixelFormat = PixelFormat.Format32bppArgb;
                    break;

                case ChannelType.Single8bpp:
                    clImageFormat = new OpenCL.Net.ImageFormat(ChannelOrder.R, Net.ChannelType.Unsigned_Int8);
                    pixelFormat = PixelFormat.Format8bppIndexed;
                    break;
            }

            ready = false;
            
            original = bitmap;
            this.Width = bitmap.Width;
            this.Height = bitmap.Height;
            this.iOMode = iOMode;

            //Lock bitmap
            switch (iOMode)
            {
                case IOMode.ReadOnly:
                    bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), 
                        ImageLockMode.ReadOnly, pixelFormat);
                    break;

                case IOMode.WriteOnly:
                    bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), 
                        ImageLockMode.WriteOnly, pixelFormat);
                    break;

                case IOMode.ReadWrite:
                    bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), 
                        ImageLockMode.ReadWrite, pixelFormat);
                    break;
            }
            
            //Get image properties
            this.Stride = bitmapData.Stride;
            this.Size = Stride * Height;
            
            //Crate cl memory object
            switch (iOMode)
            {
                default:
                case IOMode.ReadOnly:
                    clMem = Cl.CreateImage2D(context, MemFlags.UseHostPtr | MemFlags.ReadOnly, clImageFormat,
                                             (IntPtr)Width, (IntPtr)Height, (IntPtr)0,
                                             bitmapData.Scan0, out error);
                    break;

                case IOMode.WriteOnly:
                    clMem = Cl.CreateImage2D(context, MemFlags.AllocHostPtr | MemFlags.WriteOnly, clImageFormat,
                                             (IntPtr)Width, (IntPtr)Height, (IntPtr)0,
                                             null, out error);
                    break;

                case IOMode.ReadWrite:
                    clMem = Cl.CreateImage2D(context, MemFlags.UseHostPtr | MemFlags.ReadWrite, clImageFormat,
                                             (IntPtr)Width, (IntPtr)Height, (IntPtr)0,
                                             bitmapData.Scan0, out error);
                    break;
            }

            //Check error
            if (error == ErrorCode.Success)
                ready = true;
            else
                UnlockBits();
        }

        public void WriteToOriginalBitmap(byte[] source)
        {
            //Check ready
            if (!ready)
                return;

            //Cannot write to R only memory.
            if (iOMode == IOMode.ReadOnly)
                return;

            //Copy to scan0
            int size = source.Length > this.Size ? this.Size : source.Length;
            Marshal.Copy(source, 0, bitmapData.Scan0, size);
        }

        public void UnlockBits()
        {
            if (bitmapData != null)
                original.UnlockBits(bitmapData);
        }

        public void Dispose()
        {
            UnlockBits();

            original = null;
            
            if (clMem != null)
            {
                Cl.ReleaseMemObject(clMem);
                clMem = null;
            }

            if (bitmapData != null)
                bitmapData = null;
            
            ready = false;
        }
    }
}

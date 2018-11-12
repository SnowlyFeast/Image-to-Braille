using System;
using OpenCL.Net;

namespace OpenCL.Components
{
    class ClBuffer<T> : IDisposable where T : struct

    {
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

        private IMem clMem;
        private T[] buffer;
        private bool ready;

        public ClBuffer(Context context, int size, out ErrorCode error)
        {
            ready = false;

            this.Size = size;
            buffer = new T[size];
            clMem = Cl.CreateBuffer<T>(context, MemFlags.UseHostPtr, buffer, out error);

            if (error == ErrorCode.Success)
                ready = true;
        }

        public T[] GetBuffer()
        {
            return buffer;
        }

        public void Dispose()
        {
            if (!ready)
                return;

            Cl.ReleaseMemObject(clMem);
            ready = false;
        }
    }
}

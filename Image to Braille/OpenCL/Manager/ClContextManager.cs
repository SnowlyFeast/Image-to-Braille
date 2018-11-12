using System;

using OpenCL.Net;

namespace OpenCL.Manager
{
    static class ClContextManager
    {
        public static Context CreateContext(Device device, out ErrorCode error)
        {
            return Cl.CreateContext(null, 1, new Device[] { device }, null, IntPtr.Zero, out error);
        }

        public static CommandQueue CreateCommandQueue(Context context, Device device, out ErrorCode error)
        {
            return Cl.CreateCommandQueue(context, device, CommandQueueProperties.None, out error);
        }

        public static Program BuildProgram(Context context, Device device, string programText, out ErrorCode error)
        {
            OpenCL.Net.Program program = Cl.CreateProgramWithSource(context, 1, new[] { programText }, null, out error);
            error |= Cl.BuildProgram(program, 1, new[] { device }, string.Empty, null, IntPtr.Zero);

            return program;
        }

    }
}

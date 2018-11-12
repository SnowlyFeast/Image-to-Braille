namespace OpenCL.Components
{
    struct ClDeviceIndex
    {
        public int Platform { get { return platform_index; } }
        public int Device { get { return device_index; } }

        private readonly int platform_index;
        private readonly int device_index;

        public ClDeviceIndex(int PlatformIndex, int DeviceIndex)
        {
            this.platform_index = PlatformIndex;
            this.device_index = DeviceIndex;
        }
    }
}

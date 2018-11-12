using System;
using System.Collections.Generic;
using OpenCL.Net;
using OpenCL.Components;

namespace OpenCL.Manager
{
    static class ClDeviceManager
    {
        //Device validation
        public static bool ValidateDevice(ClDeviceIndex targetDeviceIndex)
        {
            int platformIndex = targetDeviceIndex.Platform;
            int deviceIndex = targetDeviceIndex.Device;

            //Default platform
            if (platformIndex == 0)
                platformIndex = 1;

            //PlatformIndex out of range
            Platform[] platformList = GetPlatform(out ErrorCode error);
            if (platformList.Length < platformIndex)
                return false;
            

            Device targetDevice;
            if (deviceIndex == 0)
            {
                //Default device
                targetDevice = GetDefaultDevice(platformList[platformIndex - 1], out error);
            }
            else
            {
                //DeviceIndex out of range
                Device[] deviceList = GetDevice(platformList[platformIndex - 1], out error);
                if (deviceList.Length < deviceIndex)
                    return false;

                targetDevice = deviceList[deviceIndex - 1];
            }
            
            //Check DeviceInfo
            bool deviceIsAvailable = CheckAvailable(targetDevice, out error) &&
                                     CheckComplilerAvailable(targetDevice, out error) &&
                                     CheckImageSupport(targetDevice, out error);

            //Check error
            return (error == ErrorCode.Success) && deviceIsAvailable;
        }

        private static bool CheckAvailable(Device device, out ErrorCode error)
        {
            //Check device is in available status.
            if (Cl.GetDeviceInfo(device, DeviceInfo.Available, out error).CastTo<Bool>() == Bool.True)
                return true;
            else
                return false;
        }

        private static bool CheckComplilerAvailable(Device device, out ErrorCode error)
        {
            if (Cl.GetDeviceInfo(device, DeviceInfo.CompilerAvailable, out error).CastTo<Bool>() == Bool.True)
                return true;
            else
                return false;
        }

        private static bool CheckImageSupport(Device device, out ErrorCode error)
        {
            //Check device is supporting Image
            if (Cl.GetDeviceInfo(device, DeviceInfo.ImageSupport, out error).CastTo<Bool>() == Bool.True)
                return true;
            else
                return false;
        }


        //Platform and device Listing
        public static Platform[] GetPlatform(out ErrorCode error)
        {
            //Return list of available platform
            Platform[] platformList = Cl.GetPlatformIDs(out error);
            
            return platformList;
        }

        public static Device[] GetDevice(Platform platform, out ErrorCode error)
        {
            //Return list of available device in target platform
            Device[] deviceList = Cl.GetDeviceIDs(platform, DeviceType.All, out error);

            return deviceList;
        }

        public static Device GetDefaultDevice(Platform platform, out ErrorCode error)
        {
            //Return default device in target platform
            Device[] deviceList = Cl.GetDeviceIDs(platform, DeviceType.Default, out error);

            return deviceList[0];
        }

        public static DeviceType GetDeviceType(Device device, out ErrorCode error)
        {
            //Return device type
            return Cl.GetDeviceInfo(device, DeviceInfo.Type, out error).CastTo<DeviceType>();
        }

        
        //Internal error handled method
        public static string[] GetNullablePlatformNameList()
        {
            //Return name list of available platform
            Platform[] platformList = GetPlatform(out ErrorCode error);

            //Check error
            if (error != ErrorCode.Success)
                return null;

            List<string> nameList = new List<string>();
            foreach (Platform p in platformList)
                nameList.Add(Cl.GetPlatformInfo(p, PlatformInfo.Name, out error).ToString());

            //Check error
            if (error != ErrorCode.Success)
                return null;
            else
                return nameList.ToArray();
        }
        public static string[] GetNullableDeviceNameList(int platformIndex)
        {
            Platform platform = GetPlatform(out ErrorCode error)[platformIndex];

            //Check error
            if (error != ErrorCode.Success)
                return null;

            Device[] deviceList = GetDevice(platform, out error);

            //Check error
            if (error != ErrorCode.Success)
                return null;

            List<string> nameList = new List<string>();
            foreach (Device d in deviceList)
                nameList.Add(Cl.GetDeviceInfo(d, DeviceInfo.Type, out error).CastTo<DeviceType>().ToString() + ":" +
                    Cl.GetDeviceInfo(d, DeviceInfo.Name, out error).ToString());

            //Check error
            if (error != ErrorCode.Success)
                return null;
            else
                return nameList.ToArray();
        }

        
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using OpenCL.Components;
using OpenCL.Manager;

namespace Image_to_Braille
{
    static class ConfigManager
    {
        //Remove local config
        public static void RemoveConfig(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        //Load config and return target platform & device index 
        public static ClDeviceIndex LoadConfig(string fileName)
        {
            //Cheak if config file exist
            if (!File.Exists(fileName))
            {
                CreateDefaultConfig(fileName);
            }

            try
            {
                using(StreamReader reader = new StreamReader(fileName))
                {
                    string line = reader.ReadLine();
                    string mac = "null";
                    int platformIndex = -1;
                    int deviceIndex = -1;

                    while(line != null)
                    {
                        //Remove spacing char
                        line = line.Replace(" ", string.Empty);

                        //Remove comment
                        if (line.Contains("//"))
                        {
                            int index = line.IndexOf('/');

                            //Continue if whole line is a comment.
                            if(index == 0)
                            {
                                line = reader.ReadLine();
                                continue;
                            }
                            line = line.Substring(0, index);
                        }

                        //Split item and value.
                        string[] spliter = line.Split('=');

                        switch (spliter[0])
                        {
                            case "MAC":
                                mac = spliter[1];
                                break;

                            case "Platform":
                                platformIndex = int.Parse(spliter[1]);
                                break;

                            case "DeviceNumber":
                                deviceIndex = int.Parse(spliter[1]);
                                break;

                            default:
                                break;
                        }

                        line = reader.ReadLine();
                    }

                    if (mac.Equals("null") || !mac.Equals(GetMacAddress()))
                        return new ClDeviceIndex(-1, -1);
                    else
                        return new ClDeviceIndex(platformIndex, deviceIndex);
                }
            }catch(Exception e)
            {
                throw e;
            }
            
        }

        //Create platform and device list text file.
        private static void CreateDefaultConfig(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    //Get platform list
                    string[] platform = ClDeviceManager.GetNullablePlatformNameList();
                    if (platform == null)
                    {
                        writer.WriteLine("//This program is not available on this platform.");
                        return;
                    }
                    
                    //Write platform
                    for (int i = 0; i < platform.Length; i++) 
                    {
                        writer.WriteLine("//" + (i + 1) + "." + platform[i]);

                        //Write each device in platform
                        int index = 1;
                        string[] device = ClDeviceManager.GetNullableDeviceNameList(index-1);

                        if (device == null)
                            break;

                        foreach (string d in device)
                        {
                            writer.WriteLine("//\t" + index + "." + d);
                            index++;
                        }
                        writer.WriteLine("//");
                    }

                    //Create default config
                    writer.WriteLine("//The MAC address verifies that the config was created by this computer.");
                    writer.WriteLine("//0 for default program settings.");
                    writer.WriteLine("MAC = " + GetMacAddress());
                    writer.WriteLine("Platform = 0");
                    writer.WriteLine("DeviceNumber = 0");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static String GetMacAddress()
        {
            //Get computer first MAC
            return NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();
        }
    }
}

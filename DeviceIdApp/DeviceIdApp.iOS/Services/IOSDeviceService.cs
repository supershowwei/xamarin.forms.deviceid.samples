using System;
using System.Runtime.InteropServices;
using DeviceIdApp.iOS.Services;
using DeviceIdApp.Protocol;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(IOSDeviceService))]

namespace DeviceIdApp.iOS.Services
{
    public class IOSDeviceService : IDeviceService
    {
        public string GetDeviceId()
        {
            return UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();

            // 從 iOS8 開始無法在實機上取得 Serial Number
            var serial = string.Empty;
            var platformExpert = IOServiceGetMatchingService(0, IOServiceMatching("IOPlatformExpertDevice"));
            if (platformExpert == 0)
            {
                return serial;
            }

            var key = (NSString)"IOPlatformSerialNumber";
            var serialNumber = IORegistryEntryCreateCFProperty(platformExpert, key.Handle, IntPtr.Zero, 0);
            if (serialNumber != IntPtr.Zero)
            {
                serial = NSString.FromHandle(serialNumber);
            }

            IOObjectRelease(platformExpert);

            return serial;
        }

        [DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        private static extern uint IOServiceGetMatchingService(uint masterPort, IntPtr matching);

        [DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        private static extern IntPtr IOServiceMatching(string s);

        [DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        private static extern IntPtr IORegistryEntryCreateCFProperty(
            uint entry,
            IntPtr key,
            IntPtr allocator,
            uint options);

        [DllImport("/System/Library/Frameworks/IOKit.framework/IOKit")]
        private static extern int IOObjectRelease(uint o);
    }
}
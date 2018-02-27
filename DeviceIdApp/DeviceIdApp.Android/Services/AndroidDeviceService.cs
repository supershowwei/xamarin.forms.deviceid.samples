using Android.App;
using Android.Provider;
using DeviceIdApp.Protocol;

namespace DeviceIdApp.Droid.Services
{
    public class AndroidDeviceService : IDeviceService
    {
        public string GetDeviceId()
        {
            return Settings.Secure.GetString(Application.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}
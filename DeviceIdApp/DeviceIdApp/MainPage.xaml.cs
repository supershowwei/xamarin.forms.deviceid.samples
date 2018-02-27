using DeviceIdApp.Protocol;
using Xamarin.Forms;

namespace DeviceIdApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var deviceService = DependencyService.Get<IDeviceService>();

            var deviceId = deviceService.GetDeviceId();

            this.DeviceIdLabel.Text = deviceId;
        }
    }
}
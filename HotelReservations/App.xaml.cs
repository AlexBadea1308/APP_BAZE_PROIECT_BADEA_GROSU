using System.Windows;

namespace HotelReservations
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DataUtil.LoadData();
        }
    }
}

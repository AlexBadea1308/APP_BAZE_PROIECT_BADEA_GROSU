using HotelReservations.Model;
using HotelReservations.ViewModel;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class Rooms : Window
    {
        public Rooms()
        {
            InitializeComponent();
            var viewModel = new RoomsViewModel();
            DataContext = viewModel;
        }
    }
}
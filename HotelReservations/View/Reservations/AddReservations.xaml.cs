using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddReservations : Window
    {
        public AddReservationsViewModel ViewModel { get; private set; }

        public AddReservations(Reservation reservation = null)
        {
            InitializeComponent();
            ViewModel = new AddReservationsViewModel(reservation);
            DataContext = ViewModel;
        }
    }
}
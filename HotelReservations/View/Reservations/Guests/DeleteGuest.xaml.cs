using HotelReservations.Model;
using HotelReservations.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeleteGuest : Window
    {
        public DeleteGuest(Guest guest, ObservableCollection<Guest> guestsList)
        {
            InitializeComponent();
            DataContext = new DeleteGuestViewModel(guest, guestsList, () =>
            {
                DialogResult = true;
                Close();
            });
        }
    }
}
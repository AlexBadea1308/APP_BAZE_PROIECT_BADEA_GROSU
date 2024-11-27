using HotelReservations.Model;
using HotelReservations.Service;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeleteGuest : Window
    {
        private GuestService guestService;
        private Guest guestToDelete;
        public DeleteGuest(Guest guest)
        {
            InitializeComponent();
            guestService = new GuestService();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

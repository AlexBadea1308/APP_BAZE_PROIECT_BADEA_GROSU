using HotelReservations.Model;
using HotelReservations.Service;
using System.Windows;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for DeleteGuest.xaml
    /// </summary>
    public partial class DeleteGuest : Window
    {
        private GuestService guestService;
        private Guest guestToDelete;
        public DeleteGuest(Guest guest)
        {
            InitializeComponent();
            guestService = new GuestService();
            guestToDelete = guest;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // add modal
            guestService.DeletePriceFromDatabase(guestToDelete);
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

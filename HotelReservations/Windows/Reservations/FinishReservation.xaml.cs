using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using System.Linq;
using System.Windows;
using static ServiceStack.Diagnostics.Events;

namespace HotelReservations.Windows
{
    public partial class FinishReservation : Window
    {
        private GuestService guestService;
        private ReservationService reservationService;
        private Reservation resToFinish;
        public FinishReservation(Reservation finishReservation)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            guestService = new GuestService();
            resToFinish = finishReservation;
        }

        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            var guestsToUpdate = guestService.guestRepository.GetGuestsByReservationId(resToFinish.Id);

            // Actualizam lista de guests
            foreach (Guest guest in guestsToUpdate)
            {
                guestService.guestRepository.Delete(guest.ReservationId);  // stergem guest care era corespondent rezervarii
            }
            reservationService.GetReservationRepository().Delete(resToFinish.Id);

            var totalPrice = reservationService.FinishReservation(resToFinish);

            MessageBox.Show($"You must pay: {totalPrice}", "Payment Information", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

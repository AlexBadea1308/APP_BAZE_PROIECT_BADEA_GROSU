using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservations.Windows
{
    public partial class DeleteReservations : Window
    {
        private GuestService guestService;
        private ReservationService reservationService;
        private Reservation resToDelete;

        public DeleteReservations(Reservation res)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            guestService = new GuestService();
            resToDelete = res;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            // Cautam guests in baza de date care au acest ReservationId
            var guestsToUpdate = guestService.guestRepository.GetGuestsByReservationId(resToDelete.Id);

            // Actualizăm oaspeții găsiți
            foreach (Guest guest in guestsToUpdate)
            {
                guestService.guestRepository.Delete(guest.ReservationId);  // Stergem guest ul care are acest RezervationID
            }
            reservationService.DeleteRezervationFromDatabase(resToDelete);

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

using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for DeleteReservations.xaml
    /// </summary>
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

            // Căutăm oaspeții în baza de date care au acest ReservationId
            var guestsToUpdate = guestService.guestRepository.GetGuestsByReservationId(resToDelete.Id);

            // Actualizăm oaspeții găsiți
            foreach (Guest guest in guestsToUpdate)
            {
                guestService.guestRepository.Delete(guest.ReservationId);  // Actualizează fiecare oaspete
            }
            reservationService.DeleteRezervationFromDatabase(resToDelete);
            // Confirmăm că acțiunea a fost realizată cu succes
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

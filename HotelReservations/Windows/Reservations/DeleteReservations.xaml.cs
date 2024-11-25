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
        private GuestRepositoryDB guestRepository;
        private ReservationService reservationService;
        private Reservation resToDelete;

        public DeleteReservations(Reservation res)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            guestRepository = new GuestRepositoryDB();
            resToDelete = res;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Adaugă modal de confirmare aici, dacă este necesar

            // Marcați rezervarea ca inactivă
            reservationService.MakeReservationInactive(resToDelete);

            // Căutăm oaspeții în baza de date care au acest ReservationId
            var guestsToUpdate = guestRepository.GetGuestsByReservationId(resToDelete.Id);

            // Actualizăm oaspeții găsiți
            foreach (Guest guest in guestsToUpdate)
            {
                guest.IsActive = false;
                guestRepository.Update(guest);  // Actualizează fiecare oaspete
            }

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

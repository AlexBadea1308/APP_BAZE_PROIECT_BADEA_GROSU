using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class GuestService
    {
        IGuestRepository guestRepository;

        public GuestService()
        {
            guestRepository = new GuestRepositoryDB();
        }

        public void SaveGuest(Guest guest, bool editing = false)
        {
            // this means guest will be in memory because reservation isn't created yet.
            if (guest.Id == 0 && editing == false)
            {
                guest.ReservationId = 0;
                Hotel.GetInstance().Guests.Add(guest);
            }

            // otherwise, its editing guest
            else
            {
                int resId = Hotel.GetInstance().Guests.Find(g => g.Id == guest.Id).ReservationId;
                guest.ReservationId = resId;
                var index = Hotel.GetInstance().Guests.FindIndex(g => (g.Id == guest.Id)&&(g.JMBG == guest.JMBG));
                Hotel.GetInstance().Guests[index] = guest;

                guestRepository.Update(guest);
                RefreshGuestsInReservation(guest.ReservationId);
            }
        }

        public void RewriteGuestIdAfterReservationIsCreated(int newReservationId)
        {
            var guestsToRewriteId = Hotel.GetInstance().Guests.Where(guest => guest.ReservationId == 0);
            foreach (Guest guest in guestsToRewriteId)
            {
                guest.ReservationId = newReservationId;
                // it will be added to database after getting real reservation id(after reservation is created).
                guestRepository.Insert(guest);
            }
        }

        public void MakeGuestInactive(Guest guest)
        {
            var makeGuestInactive = Hotel.GetInstance().Guests.Find(g => (g.Id == guest.Id) && (g.JMBG == guest.JMBG));
            makeGuestInactive.IsActive = false;

            guestRepository.Delete(guest.Id);
            RefreshGuestsInReservation(guest.ReservationId);
        }

        // helper function for refresh in memory after database is updated/deleted :)
        public void RefreshGuestsInReservation(int forThisReservationId)
        {
            foreach (var reservation in Hotel.GetInstance().Reservations)
            {
                if (reservation.Id == forThisReservationId)
                {
                    reservation.Guests = new List<Guest>();

                    foreach (var guest in Hotel.GetInstance().Guests)
                    {
                        if (guest.ReservationId == reservation.Id)
                        {
                            reservation.Guests.Add(guest);
                        }
                    }
                }
            }
        }


    }
}

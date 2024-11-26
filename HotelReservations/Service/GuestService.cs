using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Windows;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class GuestService
    {
        public GuestRepositoryDB guestRepository;

        public GuestService()
        {
            guestRepository = new GuestRepositoryDB();
        }

        public void SaveGuest(Guest guest,bool editing = false)
        {
            // this means guest will be in memory because reservation isn't created yet.
            if (guest.Id == 0 && editing == false)
            {
                Hotel.GetInstance().Guests.Add(guest);
                guestRepository.Insert(guest);
            }

            // otherwise, its editing guest
            else
            {
                int resId = Hotel.GetInstance().Guests.Find(g => g.Id == guest.Id).ReservationId;
                guest.ReservationId = resId;
                var index = Hotel.GetInstance().Guests.FindIndex(g => (g.Id == guest.Id)&&(g.CNP == guest.CNP));
                Hotel.GetInstance().Guests[index] = guest;

                guestRepository.Update(guest);
            }
        }

        public void DeletePriceFromDatabase(Guest guest)
        {
            guestRepository.Delete(guest.Id);
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
    }
}

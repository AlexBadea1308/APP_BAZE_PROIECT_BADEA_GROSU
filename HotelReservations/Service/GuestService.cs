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
            if (guest.Id == 0 && editing == false)
            {
                Hotel.GetInstance().Guests.Add(guest);
                guestRepository.Insert(guest);
            }
            else
            {
                int resId = Hotel.GetInstance().Guests.Find(g => g.Id == guest.Id).ReservationId;
                guest.ReservationId = resId;
                var index = Hotel.GetInstance().Guests.FindIndex(g => (g.Id == guest.Id)&&(g.CNP == guest.CNP));
                Hotel.GetInstance().Guests[index] = guest;

                guestRepository.Update(guest);
            }
        }

        public void DeleteGuestFromDatabase(Guest guest)
        {
            guestRepository.Delete(guest.Id);
        }

        public void RewriteGuestIdAfterReservationIsCreated(int newReservationId)
        {
            var guestsToRewriteId = Hotel.GetInstance().Guests.Where(guest => guest.ReservationId == 0);
            foreach (Guest guest in guestsToRewriteId)
            {
                guest.ReservationId = newReservationId;
                //va adauga guest in database doar dupa ce rezervarea este adaugata pentru a ne asigura ca datele rezervarii sunt valide
                guestRepository.Insert(guest);
            }
        }
    }
}

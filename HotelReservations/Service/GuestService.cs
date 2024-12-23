using HotelReservations.Model;
using HotelReservations.Repositories;
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

        public void SaveGuest(Guest guest, bool editing = false)
        {
            using (var context = new HotelDbContext())
            {
                if (guest.Id == 0 && !editing)
                {
                    context.Guests.Add(guest);
                    context.SaveChanges();
                }
                else
                {
                    guest.Reservation = null;
                    var existingGuest = context.Guests.FirstOrDefault(g => g.Id == guest.Id);
                    if (existingGuest != null)
                    {
                        existingGuest.Name = guest.Name;
                        existingGuest.Surname = guest.Surname;
                        existingGuest.CNP = guest.CNP;
                        existingGuest.ReservationId = guest.ReservationId;

                        context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteGuestFromDatabase(Guest guest)
        {
            using (var context = new HotelDbContext())
            {
                var existingGuest = context.Guests.Find(guest.Id);
                if (existingGuest != null)
                {
                    context.Guests.Remove(existingGuest);
                    context.SaveChanges();
                }
            }
        }

        public void RewriteGuestIdAfterReservationIsCreated(int newReservationId)
        {
            using (var context = new HotelDbContext())
            {
                var guestsToRewriteId = context.Guests.Where(guest => guest.ReservationId == 0).ToList();
                foreach (Guest guest in guestsToRewriteId)
                {
                    guest.ReservationId = newReservationId;
                    context.Entry(guest).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}

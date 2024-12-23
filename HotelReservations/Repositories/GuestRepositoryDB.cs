using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HotelReservations.Model;
using HotelReservations.Data;

namespace HotelReservations.Repositories
{
    public class GuestRepositoryDB
    {
        public List<Guest> GetGuestsByReservationId(int reservationId)
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    return context.Guests
                        .Where(g => g.ReservationId == reservationId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving guests: {ex.Message}");
                return new List<Guest>();
            }
        }

        public List<Guest> GetAll()
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    return context.Guests.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all guests: {ex.Message}");
                return new List<Guest>();
            }
        }

        public int Insert(Guest guest)
        {
            using (var context = new HotelDbContext())
            {
                context.Guests.Add(guest);
                context.SaveChanges();
                return guest.Id;
            }
        }

        public void Update(Guest guest)
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    context.Entry(guest).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating guest: {ex.Message}");
            }
        }

        public void Delete(int guestId)
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    var guest = context.Guests.Find(guestId);
                    if (guest != null)
                    {
                        context.Guests.Remove(guest);
                        context.SaveChanges();
                    }


                    var guestInMemory = context.Guests.FirstOrDefault(g => g.Id == guestId);
                    if (guestInMemory != null)
                    {
                        context.Guests.Remove(guestInMemory);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting guest: {ex.Message}");
            }
        }
    }
}
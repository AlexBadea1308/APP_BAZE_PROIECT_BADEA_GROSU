using HotelReservations.Data;
using HotelReservations.Model;
using System;
using System.Linq;

namespace HotelReservations.ConfigApp
{
    public class DataUtil
    {
        public static void LoadData()
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    Console.WriteLine("Starting data loading process...");

                    // Load Room Types
                    var roomTypes = context.RoomTypes.ToList();
                    Console.WriteLine($"Loaded {roomTypes.Count} room types.");

                    // Load Rooms
                    var rooms = context.Rooms.Include("RoomType").ToList();
                    Console.WriteLine($"Loaded {rooms.Count} rooms.");

                    // Load Users
                    var users = context.Users.ToList();
                    Console.WriteLine($"Loaded {users.Count} users.");


                    // Load Prices
                    var prices = context.Prices.Include("RoomType").ToList();
                    Console.WriteLine($"Loaded {prices.Count} prices.");

                    // Load Reservations
                    var reservations = context.Reservations.ToList();
                    Console.WriteLine($"Loaded {reservations.Count} reservations.");

                    // Load Guests
                    var guests = context.Guests.Include("Reservation").ToList();
                    Console.WriteLine($"Loaded {guests.Count} guests.");

 


                    Console.WriteLine("Data loading completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading data: {ex.Message}");
            }
        }
    }
}

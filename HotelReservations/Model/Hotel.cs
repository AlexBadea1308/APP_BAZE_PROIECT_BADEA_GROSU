using System;
using System.Collections.Generic;

namespace HotelReservations.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        private string name;
        public string Name {
            get { return name; }
            set
            {
                if (value != null && value != "")
                { name = value; }
                else { throw new ArgumentException(); }
            }
        }

        public string Address { get; set; }

        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        //public List<Price> PriceList { get; set; } = new List<Price>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Price> Prices { get; set; } = new List<Price>();
        public List<ReservationType> ReservationTypes { get; set; } = new List<ReservationType>();
        public List<UserType> UserTypes { get; set; } = new List<UserType>();
        public List<Guest> Guests { get; set; } = new List<Guest>();
        
        // holding authentication of specific user in here.
        public User loggedInUser { get; set; } = new User();

        private Hotel() 
        { 
        }

        private static Hotel instance;
        public static Hotel GetInstance()
        {
            if(instance == null)
            {
                instance = new Hotel();
            }
            return instance;
        }

    }
}

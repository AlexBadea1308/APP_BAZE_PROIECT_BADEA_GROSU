using System;
using System.Collections.Generic;

namespace HotelReservations.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public ReservationType ReservationType { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TotalPrice { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsFinished { get; set; } = false;
        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.Id = Id;
            clone.RoomNumber = RoomNumber;
            clone.ReservationType = ReservationType;
            clone.Guests = Guests;
            clone.StartDateTime = StartDateTime;
            clone.EndDateTime = EndDateTime;
            clone.TotalPrice = TotalPrice;
            clone.IsActive = IsActive;
            clone.IsFinished = IsFinished;

            return clone;
        }
    }
}
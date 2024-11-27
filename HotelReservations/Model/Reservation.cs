using System;

namespace HotelReservations.Model
{
    public class Reservation
    {
        public int Id { get; set; } 
        public string RoomNumber { get; set; } 
        public string ReservationType { get; set; }  
        public DateTime StartDateTime { get; set; } 
        public DateTime EndDateTime { get; set; } 
        public double TotalPrice { get; set; } 

        public Reservation(int id, string roomNumber, string reservationType, DateTime startDateTime, DateTime endDateTime, double totalPrice)
        {
            Id = id;
            RoomNumber = roomNumber;
            ReservationType = reservationType;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TotalPrice = totalPrice;
        }

        public Reservation(){}

        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.Id = Id;
            clone.RoomNumber = RoomNumber;
            clone.ReservationType = ReservationType;
            clone.StartDateTime = StartDateTime;
            clone.EndDateTime = EndDateTime;
            clone.TotalPrice = TotalPrice;
            return clone;
        }
    }
}

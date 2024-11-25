using System;

namespace HotelReservations.Model
{
    public class Reservation
    {
        public int Id { get; set; }  // Corespunde cu reservation_id
        public string RoomNumber { get; set; }  // Corespunde cu reservation_room_number
        public string ReservationType { get; set; }  // Corespunde cu reservation_type
        public DateTime StartDateTime { get; set; }  // Corespunde cu start_date_time
        public DateTime EndDateTime { get; set; }  // Corespunde cu end_date_time
        public double TotalPrice { get; set; }  // Corespunde cu total_price
        public bool IsActive { get; set; }  // Corespunde cu reservation_is_active

        // Constructor pentru a putea crea o rezervare ușor
        public Reservation(int id, string roomNumber, string reservationType, DateTime startDateTime, DateTime endDateTime, double totalPrice, bool isActive)
        {
            Id = id;
            RoomNumber = roomNumber;
            ReservationType = reservationType;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TotalPrice = totalPrice;
            IsActive = isActive;
        }

        // Constructor fără parametri pentru cazuri de inițializare implicită
        public Reservation()
        {
        }

        // Metoda de clonare a rezervării
        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.Id = Id;
            clone.RoomNumber = RoomNumber;
            clone.ReservationType = ReservationType;
            clone.StartDateTime = StartDateTime;
            clone.EndDateTime = EndDateTime;
            clone.TotalPrice = TotalPrice;
            clone.IsActive = IsActive;
            return clone;
        }
    }
}

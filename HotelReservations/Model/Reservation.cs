using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("reservation")]
    public class Reservation
    {
        private string roomNumber = string.Empty;
        private string reservationType = string.Empty;

        [Key]
        [Column("reservation_id")]
        public int Id { get; set; }

        [Column("reservation_room_number")]
        [Required]
        public string RoomNumber
        {
            get { return roomNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Room number is required");
                }
                roomNumber = value;
            }
        }

        [Column("reservation_type")]
        [Required]
        public string ReservationType
        {
            get { return reservationType; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Reservation type is required");
                }
                reservationType = value;
            }
        }

        [Column("start_date_time")]
        [Required]
        public DateTime StartDateTime { get; set; }

        [Column("end_date_time")]
        [Required]
        public DateTime EndDateTime { get; set; }

        [Column("total_price")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value")]
        public double TotalPrice { get; set; }

        public Reservation Clone()
        {
            var clone = new Reservation
            {
                Id = Id,
                RoomNumber = RoomNumber,
                ReservationType = ReservationType,
                StartDateTime = StartDateTime,
                EndDateTime = EndDateTime,
                TotalPrice = TotalPrice
            };
            return clone;
        }
    }
}

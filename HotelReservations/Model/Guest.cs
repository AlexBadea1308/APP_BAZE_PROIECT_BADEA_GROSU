using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("guest")]
    public class Guest
    {
        private string name = string.Empty;
        private string surname = string.Empty;
        private string cnp = string.Empty;

        [Key]
        [Column("guest_id")]
        public int Id { get; set; }

        [Column("reservationID")]
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        [Column("guest_name")]
        [Required]
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name is required");
                }
                name = value;
            }
        }

        [Column("guest_surname")]
        [Required]
        public string Surname
        {
            get { return surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Surname is required");
                }
                surname = value;
            }
        }

        [Column("guest_cnp")]
        [Required]
        public string CNP
        {
            get { return cnp; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("CNP is required");
                }
                cnp = value;
            }
        }

        public Guest Clone()
        {
            var clone = new Guest();
            clone.Id = Id;
            clone.Name = Name;
            clone.Surname = Surname;
            clone.CNP = CNP;
            return clone;
        }
    }
}
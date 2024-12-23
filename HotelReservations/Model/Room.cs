using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("room")]
    public class Room
    {
        private string roomNumber = string.Empty;

        [Key]
        [Column("room_id")]
        public int Id { get; set; }

        [Column("room_number")]
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

        [Column("has_Tv")]
        [Required]
        public bool HasTV { get; set; }

        [Column("has_mini_bar")]
        [Required]
        public bool HasMiniBar { get; set; }

        [Column("room_type_id")]
        public int? RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        public override string ToString()
        {
            return $"Room number: {RoomNumber}";
        }

        public Room Clone()
        {
            var clone = new Room
            {
                Id = Id,
                RoomNumber = RoomNumber,
                HasTV = HasTV,
                HasMiniBar = HasMiniBar,
                RoomTypeId = RoomTypeId,
                RoomType = RoomType
            };
            return clone;
        }
    }
}

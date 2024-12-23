using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("room_type")]
    public class RoomType
    {
        private string name = string.Empty;

        [Key]
        [Column("room_type_id")]
        public int Id { get; set; }

        [Column("room_type_name")]
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

        public override string ToString()
        {
            return Name;
        }

        public RoomType Clone()
        {
            var clone = new RoomType
            {
                Id = Id,
                Name = Name
            };
            return clone;
        }
    }
}

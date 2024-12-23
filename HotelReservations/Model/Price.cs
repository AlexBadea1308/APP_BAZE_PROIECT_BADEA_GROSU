using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("price")]
    public class Price
    {
        [Key]
        [Column("price_id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("RoomType")]
        [Column("room_type_id")]
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }

        [Required]
        [Column("price_reservation_type")]
        public string ReservationType { get; set; }

        [Required]
        [Column("price_value")]
        public double PriceValue { get; set; } = 0;

        public Price Clone()
        {
            return new Price
            {
                Id = this.Id,
                RoomTypeId = this.RoomTypeId,
                RoomType = this.RoomType,
                ReservationType = this.ReservationType,
                PriceValue = this.PriceValue
            };
        }
    }
}

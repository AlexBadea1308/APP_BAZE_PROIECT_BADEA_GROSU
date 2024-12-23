using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Model
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("first_name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Column("last_name")]
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        [Column("CNP")]
        [Required]
        [MaxLength(13)]
        public string CNP { get; set; } = string.Empty;

        [Column("username")]
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Column("user_type")]
        [Required]
        public string UserType { get; set; }

        public User Clone()
        {
            return new User
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                CNP = this.CNP,
                Username = this.Username,
                Password = this.Password,
                UserType = this.UserType
            };
        }
    }
}

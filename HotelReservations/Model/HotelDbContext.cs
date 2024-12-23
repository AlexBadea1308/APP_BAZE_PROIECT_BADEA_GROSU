using HotelReservations.ConfigApp;
using System.Data.Entity;

namespace HotelReservations.Model
{
    public class HotelDbContext : DbContext
    {
    
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public HotelDbContext() : base("name=HotelDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map the User table
            modelBuilder.Entity<User>()
                .ToTable("user")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("user_id");

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasColumnName("first_name")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Surname)
                .HasColumnName("last_name")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.CNP)
                .HasColumnName("CNP")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnName("username")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasColumnName("user_type")
                .IsRequired();


            // Map the RoomType table
            modelBuilder.Entity<RoomType>()
                .ToTable("room_type")
                .HasKey(rt => rt.Id);

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.Id)
                .HasColumnName("room_type_id");

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.Name)
                .HasColumnName("room_type_name")
                .IsRequired();

            // Map the Room table
            modelBuilder.Entity<Room>()
                .ToTable("room")
                .HasKey(r => r.Id);

            modelBuilder.Entity<Room>()
                .Property(r => r.Id)
                .HasColumnName("room_id");

            modelBuilder.Entity<Room>()
                .Property(r => r.RoomNumber)
                .HasColumnName("room_number")
                .IsRequired();

            modelBuilder.Entity<Room>()
                .Property(r => r.HasTV)
                .HasColumnName("has_Tv")
                .IsRequired();

            modelBuilder.Entity<Room>()
                .Property(r => r.HasMiniBar)
                .HasColumnName("has_mini_bar")
                .IsRequired();

            modelBuilder.Entity<Room>()
                 .HasOptional(r => r.RoomType)
                 .WithMany()
                 .HasForeignKey(r => r.RoomTypeId);

            // Map the Price table
            modelBuilder.Entity<Price>()
                .ToTable("price")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Price>()
                .Property(p => p.Id)
                .HasColumnName("price_id");

            modelBuilder.Entity<Price>()
                .Property(p => p.PriceValue)
                .HasColumnName("price_value");

            modelBuilder.Entity<Price>()
                .Property(p => p.ReservationType)
                .HasColumnName("price_reservation_type");

            modelBuilder.Entity<Price>()
                .HasRequired(p => p.RoomType)
                .WithMany()
                .HasForeignKey(p => p.RoomTypeId)
                .WillCascadeOnDelete(false);

            // Map the Reservation table
            modelBuilder.Entity<Reservation>()
                .ToTable("reservation")
                .HasKey(res => res.Id);

            modelBuilder.Entity<Reservation>()
                .Property(res => res.Id)
                .HasColumnName("reservation_id");

            modelBuilder.Entity<Reservation>()
                .Property(res => res.RoomNumber)
                .HasColumnName("reservation_room_number")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(res => res.ReservationType)
                .HasColumnName("reservation_type")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(res => res.StartDateTime)
                .HasColumnName("start_date_time")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(res => res.EndDateTime)
                .HasColumnName("end_date_time")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(res => res.TotalPrice)
                .HasColumnName("total_price");

            // Map the Guest table
            modelBuilder.Entity<Guest>()
                .ToTable("guest")
                .HasKey(g => g.Id);

            modelBuilder.Entity<Guest>()
                .Property(g => g.Id)
                .HasColumnName("guest_id");

            modelBuilder.Entity<Guest>()
                .Property(g => g.Name)
                .HasColumnName("guest_name")
                .IsRequired();

            modelBuilder.Entity<Guest>()
                .Property(g => g.Surname)
                .HasColumnName("guest_surname")
                .IsRequired();

            modelBuilder.Entity<Guest>()
                .Property(g => g.CNP)
                .HasColumnName("guest_cnp")
                .IsRequired();

            modelBuilder.Entity<Guest>()
                .Property(g => g.ReservationId)
                .HasColumnName("reservationID");


      

            base.OnModelCreating(modelBuilder);
        }
    }
}

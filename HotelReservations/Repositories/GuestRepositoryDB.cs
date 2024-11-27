using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace HotelReservations.Repositories
{
    public class GuestRepositoryDB
    {
        public List<Guest> GetGuestsByReservationId(int rezervationId)
        {
            var guests = new List<Guest>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        SELECT guest_id, guest_name, guest_surname, guest_cnp,reservationID 
                        FROM dbo.guest 
                        WHERE reservationID = @rezervationId", conn);

                    command.Parameters.Add(new SqlParameter("@rezervationId", rezervationId));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var guest = new Guest()
                        {
                            Id = (int)reader["guest_id"],
                            Name = (string)reader["guest_name"],
                            Surname = (string)reader["guest_surname"],
                            CNP = (string)reader["guest_cnp"],
                            ReservationId = (int)reader["reservationID"]
                        };
                        guests.Add(guest);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving guests: {ex.Message}");
            }

            return guests;
        }

        public List<Guest> GetAll()
        {
            var guests = new List<Guest>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM dbo.guest", conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var guest = new Guest()
                        {
                            Id = (int)reader["guest_id"],
                            Name = (string)reader["guest_name"],
                            Surname = (string)reader["guest_surname"],
                            CNP = (string)reader["guest_cnp"],
                            ReservationId = (int)reader["reservationID"]
                        };
                        guests.Add(guest);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all guests: {ex.Message}");
            }

            return guests;
        }

        public int Insert(Guest guest)
        {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = conn.CreateCommand();
                    command.CommandText=@"
                        INSERT INTO dbo.guest (guest_name, guest_surname, guest_cnp,reservationID)
                        OUTPUT inserted.guest_id
                        VALUES (@guest_name, @guest_surname, @guest_cnp,@reservationID)";

                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_cnp", guest.CNP));
                    command.Parameters.Add(new SqlParameter("@reservationID", guest.ReservationId));

                    return (int)command.ExecuteScalar();
                }
        }

        public void Update(Guest guest)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        UPDATE dbo.guest
                        SET guest_name=@guest_name, guest_surname=@guest_surname, guest_cnp=@guest_cnp,reservationID=@reservationID
                        WHERE guest_id=@guest_id", conn);

                    command.Parameters.Add(new SqlParameter("@guest_id", guest.Id));
                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_cnp", guest.CNP));
                    command.Parameters.Add(new SqlParameter("@reservationID", guest.ReservationId));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating guest: {ex.Message}");
            }
        }

        public void Delete(int rezervationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();

                    var command = new SqlCommand(@"
                DELETE FROM dbo.guest
                WHERE reservationID = @rezervationId", conn);

                    command.Parameters.AddWithValue("@rezervationId", rezervationId);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting guest: {ex.Message}");
            }

            var guests = Hotel.GetInstance().Guests.FirstOrDefault(g => g.ReservationId == rezervationId);
            if (guests != null)
            {
                Hotel.GetInstance().Guests.Remove(guests);
            }
        }

    }
}

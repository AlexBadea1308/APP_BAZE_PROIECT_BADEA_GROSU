using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace HotelReservations.Repositories
{
    public class GuestRepositoryDB : IGuestRepository
    {
        // Metoda pentru a obține oaspeții în funcție de ID-ul rezervării
        public List<Guest> GetGuestsByReservationId(int reservationId)
        {
            var guests = new List<Guest>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        SELECT guest_id, guest_name, guest_surname, guest_jmbg, guest_is_active, guest_reservation_id 
                        FROM dbo.guest 
                        WHERE guest_reservation_id = @reservationId", conn);

                    command.Parameters.Add(new SqlParameter("@reservationId", reservationId));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var guest = new Guest()
                        {
                            Id = (int)reader["guest_id"],
                            Name = (string)reader["guest_name"],
                            Surname = (string)reader["guest_surname"],
                            JMBG = (string)reader["guest_jmbg"],
                            IsActive = (bool)reader["guest_is_active"],
                            ReservationId = (int)reader["guest_reservation_id"]
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

        // Metoda pentru a obține toți oaspeții
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
                            JMBG = (string)reader["guest_jmbg"],
                            IsActive = (bool)reader["guest_is_active"],
                            ReservationId = (int)reader["guest_reservation_id"]
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

        // Metoda pentru a adăuga un oaspete nou
        public int Insert(Guest guest)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        INSERT INTO dbo.guest (guest_name, guest_surname, guest_jmbg, guest_is_active, guest_reservation_id)
                        OUTPUT inserted.guest_id
                        VALUES (@guest_name, @guest_surname, @guest_jmbg, @guest_is_active, @guest_reservation_id)", conn);

                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_jmbg", guest.JMBG));
                    command.Parameters.Add(new SqlParameter("@guest_is_active", guest.IsActive));
                    command.Parameters.Add(new SqlParameter("@guest_reservation_id", guest.ReservationId));

                    return (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting guest: {ex.Message}");
                return -1;
            }
        }

        // Metoda pentru a actualiza un oaspete existent
        public void Update(Guest guest)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        UPDATE dbo.guest
                        SET guest_name=@guest_name, guest_surname=@guest_surname, guest_jmbg=@guest_jmbg, guest_is_active=@guest_is_active, guest_reservation_id=@guest_reservation_id
                        WHERE guest_id=@guest_id", conn);

                    command.Parameters.Add(new SqlParameter("@guest_id", guest.Id));
                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_jmbg", guest.JMBG));
                    command.Parameters.Add(new SqlParameter("@guest_is_active", guest.IsActive));
                    command.Parameters.Add(new SqlParameter("@guest_reservation_id", guest.ReservationId));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating guest: {ex.Message}");
            }
        }

        // Metoda pentru a marca un oaspete ca inactiv (nu șterge din baza de date)
        public void Delete(int guestId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        UPDATE dbo.guest
                        SET guest_is_active = 0
                        WHERE guest_id = @guest_id", conn);

                    command.Parameters.Add(new SqlParameter("@guest_id", guestId));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting guest: {ex.Message}");
            }
        }
    }
}

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
        public List<Guest> GetGuestsByReservationId(int rezervationId)
        {
            var guests = new List<Guest>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        SELECT guest_id, guest_name, guest_surname, guest_jmbg, guest_is_active,reservationID 
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
                            JMBG = (string)reader["guest_jmbg"],
                            IsActive = (bool)reader["guest_is_active"],
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

        // Metoda pentru a adăuga un oaspete nou
        public int Insert(Guest guest)
        {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = conn.CreateCommand();
                    command.CommandText=@"
                        INSERT INTO dbo.guest (guest_name, guest_surname, guest_jmbg, guest_is_active,reservationID)
                        OUTPUT inserted.guest_id
                        VALUES (@guest_name, @guest_surname, @guest_jmbg, @guest_is_active, @reservationID)";

                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_jmbg", guest.JMBG));
                    command.Parameters.Add(new SqlParameter("@guest_is_active", guest.IsActive));
                    command.Parameters.Add(new SqlParameter("@reservationID", guest.ReservationId));

                    return (int)command.ExecuteScalar();
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
                        SET guest_name=@guest_name, guest_surname=@guest_surname, guest_jmbg=@guest_jmbg, guest_is_active=@guest_is_active, reservationID=@reservationID
                        WHERE guest_id=@guest_id", conn);

                    command.Parameters.Add(new SqlParameter("@guest_id", guest.Id));
                    command.Parameters.Add(new SqlParameter("@guest_name", guest.Name));
                    command.Parameters.Add(new SqlParameter("@guest_surname", guest.Surname));
                    command.Parameters.Add(new SqlParameter("@guest_jmbg", guest.JMBG));
                    command.Parameters.Add(new SqlParameter("@guest_is_active", guest.IsActive));
                    command.Parameters.Add(new SqlParameter("@reservationID", guest.ReservationId));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating guest: {ex.Message}");
            }
        }

        // Metoda pentru a marca un oaspete ca inactiv (nu șterge din baza de date)
        public void Delete(int rezervationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();

                    // Comanda SQL corectă
                    var command = new SqlCommand(@"
                DELETE FROM dbo.guest
                WHERE reservationID = @rezervationId", conn);

                    // Adaugă parametrul pentru comanda SQL
                    command.Parameters.AddWithValue("@rezervationId", rezervationId);

                    // Execută comanda
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Loghează eroarea pentru debug
                Console.WriteLine($"Error deleting guest: {ex.Message}");
            }
        }

    }
}

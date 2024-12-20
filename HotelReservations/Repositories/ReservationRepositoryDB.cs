﻿using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace HotelReservations.Repositories
{
    public class ReservationRepositoryDB
    {

        public List<Reservation> GetAll()
        {
            try
            {
                var reservations = new List<Reservation>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.reservation";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "reservation");

                    foreach (DataRow row in dataSet.Tables["reservation"]!.Rows)
                    {
                        var reservation = new Reservation()
                        {
                            Id = (int)row["reservation_id"],
                            RoomNumber = row["reservation_room_number"] as string,
                            StartDateTime = (DateTime)row["start_date_time"], 
                            EndDateTime = (DateTime)row["end_date_time"], 
                            TotalPrice = (double)row["total_price"],
                        };
                        if (Enum.TryParse<ReservationType>(row["reservation_type"]?.ToString(), out ReservationType reservationType))
                        {
                            reservation.ReservationType = reservationType.ToString();
                        }

                        reservations.Add(reservation);
                    }
                }

                return reservations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public int Insert(Reservation res)
        {
            // Validam start date si end date
            if (res.StartDateTime < new DateTime(1753, 1, 1) || res.StartDateTime > new DateTime(9999, 12, 31))
            {
                throw new InvalidOperationException("StartDateTime is out of range for SQL Server DATETIME.");
            }

            if (res.EndDateTime < new DateTime(1753, 1, 1) || res.EndDateTime > new DateTime(9999, 12, 31))
            {
                throw new InvalidOperationException("EndDateTime is out of range for SQL Server DATETIME.");
            }

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
            INSERT INTO dbo.reservation (reservation_room_number, reservation_type, start_date_time, end_date_time, total_price)
            OUTPUT inserted.reservation_id
            VALUES (@reservation_room_number, @reservation_type, @start_date_time, @end_date_time, @total_price)";

                command.Parameters.Add(new SqlParameter("reservation_room_number", res.RoomNumber));
                command.Parameters.Add(new SqlParameter("reservation_type", res.ReservationType.ToString()));
                command.Parameters.Add(new SqlParameter("start_date_time", res.StartDateTime));
                command.Parameters.Add(new SqlParameter("end_date_time", res.EndDateTime));
                command.Parameters.Add(new SqlParameter("total_price", res.TotalPrice));

                return (int)command.ExecuteScalar();
            }
        }


        public void Update(Reservation res)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.reservation
                    SET reservation_room_number=@reservation_room_number, reservation_type=@reservation_type, start_date_time=@start_date_time, end_date_time=@end_date_time,
                        total_price=@total_price
                    WHERE reservation_id=@reservation_id
                ";

                command.Parameters.Add(new SqlParameter("reservation_id", res.Id));
                command.Parameters.Add(new SqlParameter("reservation_room_number", res.RoomNumber));
                command.Parameters.Add(new SqlParameter("reservation_type", res.ReservationType.ToString()));
                command.Parameters.Add(new SqlParameter("start_date_time", res.StartDateTime));
                command.Parameters.Add(new SqlParameter("end_date_time", res.EndDateTime));
                command.Parameters.Add(new SqlParameter("total_price", res.TotalPrice));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int resId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    DELETE dbo.reservation
                    WHERE reservation_id = @resId
                ";

                command.Parameters.Add(new SqlParameter("@resId", resId));

                command.ExecuteNonQuery();
            }

            var reservation = Hotel.GetInstance().Reservations.FirstOrDefault(r => r.Id == resId);
            if (reservation != null)
            {
                Hotel.GetInstance().Reservations.Remove(reservation);
            }
        }

    }
}

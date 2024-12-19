using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using HotelReservations.Windows;

namespace HotelReservations.Repositories
{
    public class PriceRepositoryDB
    {

        public List<Price> GetPricesByRoomTypesID(int room_type_id)
        {
            try
            {
                var prices = new List<Price>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                      SELECT p.*, rt.* " +
                      "FROM dbo.price p\r\nINNER JOIN dbo.room_type rt " +
                      "ON p.room_type_id = @room_type_id", conn);

                    command.Parameters.Add(new SqlParameter("@room_type_id", room_type_id));

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "price");
                    foreach (DataRow row in dataSet.Tables["price"]!.Rows)
                    {
                        var price = new Price()
                        {
                            Id = (int)row["price_id"],
                            PriceValue = (double)row["price_value"],
                            RoomType = new RoomType()
                            {
                                Id = (int)row["room_type_id"],
                                Name = (string)row["room_type_name"],
                            }
                        };
                        if (Enum.TryParse<ReservationType>(row["price_reservation_type"]?.ToString(), out ReservationType resType))
                        {
                            price.ReservationType = resType;
                        }

                        prices.Add(price);
                    }
                }
                return prices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Price> GetAll()
        {
            try
            {
                var prices = new List<Price>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();
                    var commandText = new SqlCommand(@"SELECT p.*, rt.* " +
                                      "FROM dbo.price p\r\nINNER JOIN dbo.room_type rt " +
                                      "ON p.room_type_id = rt.room_type_id", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "price");
                    foreach (DataRow row in dataSet.Tables["price"]!.Rows)
                       { 
                            var price = new Price()
                            {
                                Id = (int)row["price_id"],
                                PriceValue = (double)row["price_value"],
                                RoomType = new RoomType()
                                {
                                    Id = (int)row["room_type_id"],
                                    Name = (string)row["room_type_name"],
                                }
                            };
                        if (Enum.TryParse<ReservationType>(row["price_reservation_type"]?.ToString(), out ReservationType resType))
                        {
                            price.ReservationType = resType;
                        }

                        prices.Add(price);
                        }
                }

                return prices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int Insert(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.price (price_value, price_reservation_type, room_type_id)
                    OUTPUT inserted.price_id
                    VALUES (@price_value, @price_reservation_type, @room_type_id)
                ";

                command.Parameters.Add(new SqlParameter("room_type_id", price.RoomType.Id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.ReservationType.ToString()));
                command.Parameters.Add(new SqlParameter("price_value", price.PriceValue));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.price 
                    SET price_value=@price_value, price_reservation_type=@price_reservation_type, room_type_id=@room_type_id
                    WHERE price_id=@price_id
                ";

                command.Parameters.Add(new SqlParameter("price_id", price.Id));
                command.Parameters.Add(new SqlParameter("room_type_id", price.RoomType.Id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.ReservationType.ToString()));
                command.Parameters.Add(new SqlParameter("price_value", price.PriceValue));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int priceId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    DELETE dbo.price
                    WHERE price_id = @price_id
                ";

                command.Parameters.Add(new SqlParameter("price_id", priceId));

                command.ExecuteNonQuery();

            }

            var prices = Hotel.GetInstance().Prices.FirstOrDefault(p => p.Id == priceId);
            if (prices != null)
            {
                Hotel.GetInstance().Prices.Remove(prices);
            }
        }
    }
}

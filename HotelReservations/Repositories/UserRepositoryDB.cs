using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace HotelReservations.Repositories
{
    public class UserRepositoryDB
    {
        public List<User> GetAll()
        {
            try
            {
                var users = new List<User>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.[user]";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "user");

                    foreach (DataRow row in dataSet.Tables["user"]!.Rows)
                    {
                        var user = new User()
                        {
                            Id = (int)row["user_id"],
                            Name = (string)row["first_name"],
                            Surname = (string)row["last_name"],
                            CNP = (string)row["CNP"],
                            Password = (string)row["password"],
                            Username = (string)row["username"],
                        };
                        if (Enum.TryParse<UserType>(row["user_type"]?.ToString(), out UserType userType))
                        {
                            user.UserType = userType;
                        }

                        users.Add(user);
                    }
                }
 
                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int Insert(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.[user] (first_name, last_name, CNP, username, password,user_type)
                    OUTPUT inserted.user_id
                    VALUES (@first_name, @last_name, @CNP, @username, @password, @user_type)
                ";

                command.Parameters.Add(new SqlParameter("first_name", user.Name));
                command.Parameters.Add(new SqlParameter("last_name", user.Surname));
                command.Parameters.Add(new SqlParameter("CNP", user.CNP));
                command.Parameters.Add(new SqlParameter("username", user.Username));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType.ToString()));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.[user] 
                    SET first_name=@first_name, last_name=@last_name, CNP=@CNP, username=@username, password=@password, user_type=@user_type
                    WHERE user_id=@user_id
                ";

                command.Parameters.Add(new SqlParameter("user_id", user.Id));
                command.Parameters.Add(new SqlParameter("first_name", user.Name));
                command.Parameters.Add(new SqlParameter("last_name", user.Surname));
                command.Parameters.Add(new SqlParameter("CNP", user.CNP));
                command.Parameters.Add(new SqlParameter("username", user.Username));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType.ToString()));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int userId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    DELETE dbo.[user]
                    WHERE user_id = @user_id
                ";

                command.Parameters.Add(new SqlParameter("user_id", userId));

                command.ExecuteNonQuery();

            }

            var users = Hotel.GetInstance().Users.FirstOrDefault(u => u.Id == userId);
            if (users != null)
            {
                Hotel.GetInstance().Users.Remove(users);
            }
        }
    }
}

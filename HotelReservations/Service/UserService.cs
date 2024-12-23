using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class UserService
    {

        public List<User> GetAllUsers()
        {
            using (var context = new HotelDbContext())
            {
                return context.Users.ToList();
            }
        }

        public void SaveUser(User user)
        {
            using (var context = new HotelDbContext())
            {
                if (user.Id == 0)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    user.Id = user.Id;
                }
                else
                {
                    var existingUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Name = user.Name;
                        existingUser.Surname = user.Surname;
                        existingUser.CNP = user.CNP;
                        existingUser.Username = user.Username;
                        existingUser.Password = user.Password;
                        existingUser.UserType = user.UserType;

                        context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteUserFromDatabase(User user)
        {
            using (var context = new HotelDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    context.Users.Remove(existingUser);
                    context.SaveChanges();
                }
            }
        }
    }
}

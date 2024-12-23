using HotelReservations.Model;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Repositories
{
    public class UserRepositoryDB
    {
        public List<User> GetAll()
        {
            using (var context = new HotelDbContext())
            {
                return context.Users.ToList();
            }
        }

        public User GetById(int id)
        {
            using (var context = new HotelDbContext())
            {
                return context.Users.SingleOrDefault(u => u.Id == id);
            }
        }

        public int Insert(User user)
        {
            using (var context = new HotelDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return user.Id;
        }

        public void Update(User user)
        {
            using (var context = new HotelDbContext())
            {
                var existingUser = context.Users.SingleOrDefault(u => u.Id == user.Id);
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

        public void Delete(int id)
        {
            using (var context = new HotelDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Id == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }
    }
}

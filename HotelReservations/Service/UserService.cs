using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class UserService
    {
        public UserRepositoryDB userRepository;
        public UserService()
        {
            userRepository = new UserRepositoryDB();
        }

        public List<User> GetAllUsers()
        {
            return Hotel.GetInstance().Users;
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                Hotel.GetInstance().Users.Add(user);
                user.Id = userRepository.Insert(user);
            }
            else
            {
                var index = Hotel.GetInstance().Users.FindIndex(u => u.Id == user.Id);
                Hotel.GetInstance().Users[index] = user;
                userRepository.Update(user);
            }
        }

        public void DeleteUserFromDatabase(User user)
        {
            userRepository.Delete(user.Id);
        }
    }
}

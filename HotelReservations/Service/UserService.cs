using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class UserService
    {
        IUserRepository userRepository;
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

        public void MakeUserInactive(User user)
        {
            var makeUserInactive = Hotel.GetInstance().Users.Find(u => u.Id == user.Id);
            makeUserInactive.IsActive = false;
            userRepository.Delete(user.Id);
        }
    }
}

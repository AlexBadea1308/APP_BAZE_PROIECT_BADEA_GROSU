using HotelReservations.Model;
using System.Collections.Generic;

namespace HotelReservations.Repositories
{
    public interface IUserRepository
    {
        void Delete(int userId);

        List<User> GetAll();

        int Insert(User user);

        void Update(User user);
    }
}

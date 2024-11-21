using HotelReservations.Model;
using System.Collections.Generic;

namespace HotelReservations.Repositories
{
    public interface IGuestRepository
    {

        void Delete(int guestId);

        List<Guest> GetAll();

        int Insert(Guest guest);

        void Update(Guest guest);
    }
}

using HotelReservations.Model;
using System.Collections.Generic;

namespace HotelReservations.Repositories
{
    public interface IReservationRepository
    {
        void Delete(int resId);

        List<Reservation> GetAll();

        int Insert(Reservation res);

        void Update(Reservation res);
    }
}

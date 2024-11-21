using HotelReservations.Model;
using System.Collections.Generic;

namespace HotelReservations.Repositories
{
    public interface IPriceRepository
    {
        void Delete(int priceId);

        List<Price> GetAll();

        int Insert(Price price);

        void Update(Price price);
    }
}

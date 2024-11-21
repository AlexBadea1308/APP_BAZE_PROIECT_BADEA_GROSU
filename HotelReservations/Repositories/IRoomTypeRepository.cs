using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repositories
{
    public interface IRoomTypeRepository
    {
        void Delete(int roomTypeId);

        List<RoomType> GetAll();

        int Insert(RoomType roomType);

        void Update(RoomType roomType);
    }
}

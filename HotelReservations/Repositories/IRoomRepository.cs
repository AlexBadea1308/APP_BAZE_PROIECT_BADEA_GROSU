using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repository
{
    public interface IRoomRepository
    {
        void Delete(int roomId);

        List<Room> GetAll();

        int Insert(Room room);

        void Update(Room room);
    }
}

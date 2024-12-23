using HotelReservations.Model;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Repositories
{
    public class RoomTypeRepositoryDB
    {
        public List<RoomType> GetAll()
        {
            using (var context = new HotelDbContext())
            {
                return context.RoomTypes.ToList();
            }
        }

        public int Insert(RoomType roomType)
        {
            using (var context = new HotelDbContext())
            {
                context.RoomTypes.Add(roomType);
                context.SaveChanges();
                return roomType.Id;
            }
        }

        public void Update(RoomType roomType)
        {
            using (var context = new HotelDbContext())
            {
                var existingRoomType = context.RoomTypes.SingleOrDefault(rt => rt.Id == roomType.Id);
                if (existingRoomType != null)
                {
                    existingRoomType.Name = roomType.Name;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int roomTypeId)
        {
            using (var context = new HotelDbContext())
            {
                var roomType = context.RoomTypes.SingleOrDefault(rt => rt.Id == roomTypeId);
                if (roomType != null)
                {
                    context.RoomTypes.Remove(roomType);
                    context.SaveChanges();
                }
            }
        }
    }
}

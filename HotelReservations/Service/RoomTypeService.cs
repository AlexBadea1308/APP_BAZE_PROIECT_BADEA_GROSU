using HotelReservations.Repositories;
using HotelReservations.Model;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class RoomTypeService
    {
        public RoomTypeRepositoryDB roomTypeRepository;
        private RoomService roomService;

        public RoomTypeService()
        {
            roomTypeRepository = new RoomTypeRepositoryDB();
            roomService = new RoomService();
        }

        public List<RoomType> GetAllRoomTypes()
        {
            using (var context = new HotelDbContext())
            {
                return context.RoomTypes.ToList();
            }
        }

        public void SaveRoomType(RoomType roomType)
        {
            using (var context = new HotelDbContext())
            {
                if (roomType.Id == 0)
                {
                    context.RoomTypes.Add(roomType);
                    context.SaveChanges();
                }
                else
                {
                    var existingRoomType = context.RoomTypes.FirstOrDefault(rt => rt.Id == roomType.Id);
                    if (existingRoomType != null)
                    {
                        existingRoomType.Name = roomType.Name;
                        context.SaveChanges();
                    }

                    // Refresh the room types in rooms
                    var roomsToUpdate = context.Rooms.Where(r => r.RoomType.Id == roomType.Id).ToList();
                    foreach (var room in roomsToUpdate)
                    {
                        room.RoomType = roomType;
                    }

                    context.SaveChanges();
                }
            }
        }

        public RoomType GetRoomTypeByName(int roomTypeID)
        {
            using (var context = new HotelDbContext())
            {
                return context.RoomTypes.FirstOrDefault(rt => rt.Id == roomTypeID);
            }
        }

        public void DeleteRoomTypeFromDatabase(RoomType roomType)
        {
            using (var context = new HotelDbContext())
            {
                var existingRoomType = context.RoomTypes.Find(roomType.Id);
                if (existingRoomType != null)
                {
                    context.RoomTypes.Remove(existingRoomType);
                    context.SaveChanges();
                }
            }
        }

        public bool IsRoomTypeInUse(RoomType roomType)
        {
            using (var context = new HotelDbContext())
            {
                return context.Rooms.Any(r => r.RoomType.Name == roomType.Name);
            }
        }
    }
}

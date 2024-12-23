using HotelReservations.Data;
using HotelReservations.Model;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class RoomService
    {
        public List<Room> GetAllRooms()
        {
            using (var context = new HotelDbContext())
            {
                return context.Rooms.ToList();
            }
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            using (var context = new HotelDbContext())
            {
                return context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            }
        }

        public void SaveRoom(Room room)
        {
            using (var context = new HotelDbContext())
            {
                if (room.Id == 0)
                {
                    context.Rooms.Add(room);
                }
                else
                {
                    context.Entry(room).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public void DeleteRoomFromDatabase(Room room)
        {
            using (var context = new HotelDbContext())
            {
                var existingRoom = context.Rooms.Find(room.Id);
                if (existingRoom != null)
                {
                    context.Rooms.Remove(existingRoom);
                    context.SaveChanges();
                }
            }
        }

        public bool IsRoomInUse(Room room)
        {
            using (var context = new HotelDbContext())
            {
                return context.Reservations.Any(reservation => reservation.RoomNumber == room.RoomNumber);
            }
        }
    }
}

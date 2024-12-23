using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HotelReservations.Model;

namespace HotelReservations.Data
{
    public class RoomRepositoryDB
    {
        public List<Room> GetRoomsByRoomTypeID(int roomTypeId)
        {
            using (var context = new HotelDbContext())
            {
                return context.Rooms
                    .Include(r => r.RoomType)
                    .Where(r => r.RoomType.Id == roomTypeId)
                    .ToList();
            }
        }

        public List<Room> GetAll()
        {
            using (var context = new HotelDbContext())
            {
                return context.Rooms
                    .Include(r => r.RoomType)
                    .ToList();
            }
        }

        public int Insert(Room room)
        {
            using (var context = new HotelDbContext())
            {
                context.Rooms.Add(room);
                context.SaveChanges();
                return room.Id;
            }
        }

        public void Update(Room room)
        {
            using (var context = new HotelDbContext())
            {
                var existingRoom = context.Rooms.Find(room.Id);
                if (existingRoom != null)
                {
                    existingRoom.RoomNumber = room.RoomNumber;
                    existingRoom.HasTV = room.HasTV;
                    existingRoom.HasMiniBar = room.HasMiniBar;
                    existingRoom.RoomType = context.RoomTypes.Find(room.RoomType.Id);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int roomId)
        {
            using (var context = new HotelDbContext())
            {
                var room = context.Rooms.Find(roomId);
                if (room != null)
                {
                    context.Rooms.Remove(room);
                    context.SaveChanges();
                }
            }
        }
    }
}

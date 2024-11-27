using HotelReservations.Repositories;
using HotelReservations.Model;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class RoomTypeService
    {

        public RoomTypeRepositoryDB roomTypeRepository;
        RoomService roomService;

        public RoomTypeService()
        {
            roomTypeRepository = new RoomTypeRepositoryDB();
            roomService = new RoomService();

        }

        public List<RoomType> GetAllRoomTypes()
        {
            return Hotel.GetInstance().RoomTypes;
        }

        public void SaveRoomType(RoomType roomType)
        {
            if (roomType.Id == 0)
            {
                roomType.Id = roomTypeRepository.Insert(roomType);
                Hotel.GetInstance().RoomTypes.Add(roomType);
            }
            else
            {
                var rooms = Hotel.GetInstance().Rooms;
                var index = Hotel.GetInstance().RoomTypes.FindIndex(r => r.Id == roomType.Id);
                Hotel.GetInstance().RoomTypes[index] = roomType;

                //refresh la roomtype
                foreach (var room in rooms)
                {
                    if(room.RoomType.Id == roomType.Id)
                    {
                        room.RoomType = roomType;
                    }
                }

                // database update
                roomTypeRepository.Update(roomType);
            }
        }

        public RoomType GetRoomTypeByName(int roomTypeID)
        {
            var selectedRoomType = Hotel.GetInstance().RoomTypes.Find(rt => rt.Id == roomTypeID);
            return selectedRoomType!;
        }

        public void DeleteRoomTypeFromDatabase(RoomType roomType)
        {
            roomTypeRepository.Delete(roomType.Id);
        }

       //verificam daca roomtype exista deja in database
        public bool IsRoomTypeInUse(RoomType roomType)
        {
            foreach (Room room in Hotel.GetInstance().Rooms)
            {
                if (room.RoomType == roomType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

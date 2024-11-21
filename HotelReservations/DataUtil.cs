using HotelReservations.Repositories;
using HotelReservations.Exceptions;
using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using HotelReservations.Service;
using System.Collections.Generic;

namespace HotelReservations
{
    public class DataUtil
    {
        public static void LoadData()
        {
            Hotel hotel = Hotel.GetInstance();
            hotel.Id = 1;
            hotel.Name = "Hotel Park";
            hotel.Address = "Kod Futoskog parka...";

            var resTypeDay = ReservationType.Day;
            var resTypeNight = ReservationType.Night;

            var userTypeAdministrator = UserType.Administrator;
            var userTypeReceptionist = UserType.Receptionist;

            hotel.UserTypes.Add(userTypeAdministrator);
            hotel.UserTypes.Add(userTypeReceptionist);

            hotel.ReservationTypes.Add(resTypeDay);
            hotel.ReservationTypes.Add(resTypeNight);

            try
            {
                IRoomTypeRepository roomTypeRepository = new RoomTypeRepositoryDB();
                IUserRepository usersRepository = new UserRepositoryDB();
                IRoomRepository roomRepository = new RoomRepositoryDB();
                IPriceRepository priceRepository = new PriceRepositoryDB();
                IGuestRepository guestRepository = new GuestRepositoryDB();
                IReservationRepository reservationRepository = new ReservationRepositoryDB();

                var loadedRoomTypes = roomTypeRepository.GetAll();
                if (loadedRoomTypes != null)
                {
                    Hotel.GetInstance().RoomTypes = loadedRoomTypes;
                }

                var loadedRooms = roomRepository.GetAll();
                if (loadedRooms != null)
                {
                    Hotel.GetInstance().Rooms = loadedRooms;
                }

                var loadedUsers = usersRepository.GetAll();
                if (loadedUsers != null)
                {
                    Hotel.GetInstance().Users = loadedUsers;
                }

                var loadedPriceRepository = priceRepository.GetAll();
                if (loadedPriceRepository != null)
                {
                    Hotel.GetInstance().Prices = loadedPriceRepository;
                }

                var loadedGuestsRepository = guestRepository.GetAll();
                if (loadedGuestsRepository != null)
                {
                    Hotel.GetInstance().Guests = loadedGuestsRepository;
                }

                var loadedReservationRepository = reservationRepository.GetAll();
                if (loadedReservationRepository != null)
                {
                    Hotel.GetInstance().Reservations = loadedReservationRepository;
                }

                // just adding guests to specific reservation
                foreach (var reservation in Hotel.GetInstance().Reservations)
                {
                    reservation.Guests = new List<Guest>();

                    foreach (var guest in Hotel.GetInstance().Guests)
                    {
                        if (guest.ReservationId == reservation.Id)
                        {
                            reservation.Guests.Add(guest);
                        }
                    }
                }

            }
            catch (CouldntLoadResourceException)
            {
                Console.WriteLine("Call an administrator, something weird is happening with the files");
            }
            catch (Exception ex)
            {
                Console.Write("Unexpected error", ex.Message);
            }
        }
    }
}

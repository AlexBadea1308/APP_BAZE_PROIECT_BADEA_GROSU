﻿using HotelReservations.Repositories;
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
            hotel.Name = "Topolos Grand Resort";
            hotel.Address ="Strada Apollo 13";

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
                RoomTypeRepositoryDB roomTypeRepository = new RoomTypeRepositoryDB();
                UserRepositoryDB usersRepository = new UserRepositoryDB();
                RoomRepositoryDB roomRepository = new RoomRepositoryDB();
                PriceRepositoryDB priceRepository = new PriceRepositoryDB();
                GuestRepositoryDB guestRepository = new GuestRepositoryDB();
                ReservationRepositoryDB reservationRepository = new ReservationRepositoryDB();

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

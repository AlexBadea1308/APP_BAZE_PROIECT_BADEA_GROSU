﻿using HotelReservations.Model;
using HotelReservations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    internal class ReservationService
    {
        IReservationRepository reservationRepository;
        PriceService priceService;
        GuestService guestService;
        RoomService roomService;

        public ReservationService()
        {
            reservationRepository = new ReservationRepositoryDB();
            guestService = new GuestService();
            priceService = new PriceService();
            roomService = new RoomService();
        }

        public List<Reservation> GetAllReservations()
        {
            return Hotel.GetInstance().Reservations;
        }

        public void SaveReservation(Reservation reservation, Room room)
        {
            reservation.RoomNumber = room.RoomNumber;

            // checking is date equal for deciding what type reservation is. if its equal then its day, if its not equal then its night
       
            

            // if reservation id is "0"(doesnt exist yet), then its adding
            if (reservation.Id == 0)
            {
                Hotel.GetInstance().Reservations.Add(reservation);
                reservation.TotalPrice = CountPrice(reservation);

                reservation.Id = reservationRepository.Insert(reservation);

                // this will rewrite guests ID(because all have fake id(reservation not added yet so i have to rewrite all guest's reservation ID to this one ));
                guestService.RewriteGuestIdAfterReservationIsCreated(reservation.Id);
                reservation.Guests = Hotel.GetInstance().Guests.Where(guest => guest.ReservationId == reservation.Id).ToList();
            }

            // otherwise, update reservation.
            else
            {
                reservation.TotalPrice = CountPrice(reservation);
                var r = Hotel.GetInstance().Reservations.First(r => r.Id == reservation.Id);

                r.TotalPrice = reservation.TotalPrice;
                r.StartDateTime = reservation.StartDateTime;
                r.EndDateTime = reservation.EndDateTime;

                reservationRepository.Update(reservation);
            }
        }

        // delete or finishing res;
        public void MakeReservationInactive(Reservation reservation, bool finish = false)
        {
            var res = Hotel.GetInstance().Reservations.Find(r => r.Id == reservation.Id);
            res.IsActive = false;

            // so now if finish is true i will just update state otherwise i will delete(make inactive) :)
            if (finish == true)
            {
                res.IsFinished = true;
                reservationRepository.Update(res);
                return;
            }

            reservationRepository.Delete(res.Id);
        }
        public void LogAllPrices()
        {
            var prices = priceService.GetAllPrices();
            foreach (var price in prices)
            {
                Console.WriteLine("testing...");
                Console.WriteLine($"RoomType: {price.RoomType}, ReservationType: {price.ReservationType}, PriceValue: {price.PriceValue}");
            }
        }
        public double CountPrice(Reservation reservation)
        {

            var resType = reservation.ReservationType;
            int dateDifference = GetDateDifference(reservation.StartDateTime, reservation.EndDateTime);
            if (dateDifference == 0)
            {
                resType = ReservationType.Day;
            }
            else
                resType = ReservationType.Night;
            Room room = roomService.GetRoomByRoomNumber(reservation.RoomNumber);

            //System.Diagnostics.Debug.WriteLine($"Searching for RoomType: '{room.RoomType}'");
            //System.Diagnostics.Debug.WriteLine($"Searching for ReservationType: '{resType}'");

            //foreach (var p in priceService.GetAllPrices())
            //{
            //    System.Diagnostics.Debug.WriteLine($"Available - RoomType: '{p.RoomType}', ReservationType: '{p.ReservationType}', PriceValue: {p.PriceValue}");

            //    if (!p.RoomType.ToString().Equals(room.RoomType.ToString()))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"RoomType mismatch. Expected: '{room.RoomType}', Found: '{p.RoomType}'");
            //    }

            //    if (!p.ReservationType.ToString().Trim().Equals(resType.ToString().Trim()))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ReservationType mismatch. Expected: '{resType}', Found: '{p.ReservationType}'");
            //    }

            //}


            // Find the corresponding price
            Price? price = priceService.GetAllPrices().FirstOrDefault(p => p.RoomType.ToString().Equals(room.RoomType.ToString()) && p.ReservationType.ToString().Trim().Equals(resType.ToString().Trim()));

            // Dacă prețul este null, aruncăm o eroare
            if (price == null)
            {
                throw new InvalidOperationException($"Price not found for RoomType: {room.RoomType} and ReservationType: {resType}.");
            }

            // Calculate the total price
            reservation.TotalPrice = dateDifference * price.PriceValue;
            return reservation.TotalPrice;
        }

        public double FinishReservation(Reservation reservation)
        {
            MakeReservationInactive(reservation, true);
            return reservation.TotalPrice;
        }

        // count date difference for reservation
        public int GetDateDifference(DateTime start, DateTime end)
        {
            if (start.Date == end.Date)
            {
                return 1;
            }
            
            TimeSpan difference = end.Date - start.Date;
            return (int)difference.TotalDays;
        }
    

    }
}

using HotelReservations.Model;
using HotelReservations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    internal class ReservationService
    {
        public ReservationRepositoryDB reservationRepository;
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

        public void DeleteRezervationFromDatabase(Reservation rezervation)
        {
            reservationRepository.Delete(rezervation.Id);
        }
        
        public ReservationRepositoryDB GetReservationRepository()
        {
            return reservationRepository;
        }
        public void SaveReservation(Reservation reservation, Room room)
        {
            reservation.RoomNumber = room.RoomNumber;
            //verificam daca rezervarea nu exista atunci o adaugam else o actualizam
            if (reservation.Id == 0)
            {
                Hotel.GetInstance().Reservations.Add(reservation);
                reservation.TotalPrice = CountPrice(reservation);
            }
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

            int dateDifference = GetDateDifference(reservation.StartDateTime, reservation.EndDateTime);
            if (dateDifference == 0)
            {
                reservation.ReservationType = ReservationType.Day.ToString();
            }
            else
                reservation.ReservationType = ReservationType.Night.ToString();
            Room room = roomService.GetRoomByRoomNumber(reservation.RoomNumber);


            // Aflam pretul corespondent tipului de rezervare Day/Night
            Price? price = priceService.GetAllPrices().FirstOrDefault(p => p.RoomType.ToString().Equals(room.RoomType.ToString()) && p.ReservationType.ToString().Trim().Equals(reservation.ReservationType.ToString().Trim()));

            // Daca pretul este null aruncam o eroare
            if (price == null)
            {
                throw new InvalidOperationException($"Price not found for RoomType: {room.RoomType} and ReservationType: {reservation.ReservationType}.");
            }

            // Calculam pretul total
            if (dateDifference == 0)
                reservation.TotalPrice = price.PriceValue;
            else
                reservation.TotalPrice = dateDifference * price.PriceValue;

            return reservation.TotalPrice;
        }

        public double FinishReservation(Reservation reservation)
        {
            return reservation.TotalPrice;
        }

         //calculam cate zile va sta guest   
        public int GetDateDifference(DateTime start, DateTime end)
        {
            if (start.Date == end.Date)
            {
                return 0;
            }

            TimeSpan difference = end.Date - start.Date;
            return (int)difference.TotalDays;
        }
    }
}

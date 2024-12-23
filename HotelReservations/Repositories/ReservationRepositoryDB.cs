using System;
using System.Collections.Generic;
using System.Linq;
using HotelReservations.Model;
using System.Data.Entity;

namespace HotelReservations.Data
{
    public class ReservationRepositoryDB
    {
        public List<Reservation> GetAll()
        {
            using (var context = new HotelDbContext())
            {
                return context.Reservations
                    .ToList();
            }
        }

        public Reservation GetById(int reservationId)
        {
            using (var context = new HotelDbContext())
            {
                return context.Reservations
                    .FirstOrDefault(r => r.Id == reservationId);
            }
        }

        public int Insert(Reservation reservation)
        {
            using (var context = new HotelDbContext())
            {
                context.Reservations.Add(reservation);
                context.SaveChanges();
                return reservation.Id;
            }
        }

        public void Update(Reservation reservation)
        {
            using (var context = new HotelDbContext())
            {
                var existingReservation = context.Reservations.Find(reservation.Id);
                if (existingReservation != null)
                {
                    existingReservation.RoomNumber = reservation.RoomNumber;
                    existingReservation.ReservationType = reservation.ReservationType;
                    existingReservation.StartDateTime = reservation.StartDateTime;
                    existingReservation.EndDateTime = reservation.EndDateTime;
                    existingReservation.TotalPrice = reservation.TotalPrice;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int reservationId)
        {
            using (var context = new HotelDbContext())
            {
                var reservation = context.Reservations.Find(reservationId);
                if (reservation != null)
                {
                    context.Reservations.Remove(reservation);
                    context.SaveChanges();
                }
            }
        }

    }
}

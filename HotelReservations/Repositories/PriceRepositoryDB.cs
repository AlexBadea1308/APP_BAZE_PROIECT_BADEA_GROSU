using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HotelReservations.Model;

namespace HotelReservations.Data
{
    public class PriceRepositoryDB
    {
        public List<Price> GetPricesByRoomTypeId(int roomTypeId)
        {
            using (var context = new HotelDbContext())
            {
                return context.Prices
                    .Include(p => p.RoomType)
                    .Where(p => p.RoomType.Id == roomTypeId)
                    .ToList();
            }
        }

        public List<Price> GetAll()
        {
            using (var context = new HotelDbContext())
            {
                return context.Prices
                    .Include(p => p.RoomType)
                    .ToList();
            }
        }

        public int Insert(Price price)
        {
            using (var context = new HotelDbContext())
            {
                context.Prices.Add(price);
                context.SaveChanges();
                return price.Id;
            }
        }

        public void Update(Price price)
        {
            using (var context = new HotelDbContext())
            {
                var existingPrice = context.Prices.Find(price.Id);
                if (existingPrice != null)
                {
                    existingPrice.PriceValue = price.PriceValue;
                    existingPrice.ReservationType = price.ReservationType;
                    existingPrice.RoomType = price.RoomType;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int priceId)
        {
            using (var context = new HotelDbContext())
            {
                var price = context.Prices.Find(priceId);
                if (price != null)
                {
                    context.Prices.Remove(price);
                    context.SaveChanges();
                }
            }
        }
    }
}
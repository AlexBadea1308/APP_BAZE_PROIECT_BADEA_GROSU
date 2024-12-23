using HotelReservations.Data;
using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class PriceService
    {
        public PriceRepositoryDB priceRepository;

        public PriceService()
        {
            priceRepository = new PriceRepositoryDB();
        }

        public List<Price> GetAllPrices()
        {
            using (var context = new HotelDbContext())
            {
                return context.Prices.ToList();
            }
        }

        public void SavePrice(Price price)
        {
            using (var context = new HotelDbContext())
            {
                if (price.Id == 0)
                {
                    context.Prices.Add(price);
                    context.SaveChanges();
                }
                else
                {
                    var existingPrice = context.Prices.FirstOrDefault(p => p.Id == price.Id);
                    if (existingPrice != null)
                    {
                        existingPrice = price;

                        context.SaveChanges();
                    }
                }
            }
        }


        public void DeletePriceFromDatabase(Price price)
        {
            priceRepository.Delete(price.Id);
        }
    }
}

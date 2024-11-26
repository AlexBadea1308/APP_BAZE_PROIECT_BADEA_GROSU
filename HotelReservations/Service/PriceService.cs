using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

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
            //return Hotel.GetInstance().Prices;
            return Hotel.GetInstance().Prices;
        }
        public void SavePrice(Price price)
        {
            if (price.Id == 0)
            {
                Hotel.GetInstance().Prices.Add(price);
                price.Id = priceRepository.Insert(price);
            }
            else
            {
                var index = Hotel.GetInstance().Prices.FindIndex(p => p.Id == price.Id);
                Hotel.GetInstance().Prices[index] = price;
                priceRepository.Update(price);
            }
        }

        public void DeletePriceFromDatabase(Price price)
        {
            priceRepository.Delete(price.Id);
        }
    }
}

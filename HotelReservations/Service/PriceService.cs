using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class PriceService
    {
        IPriceRepository priceRepository;

        public PriceService()
        {
            priceRepository = new PriceRepositoryDB();
        }

        public List<Price> GetAllPrices()
        {
            //return Hotel.GetInstance().Prices;
            return Hotel.GetInstance().PriceList;
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

        public void MakePriceInactive(Price price)
        {
            var makePriceInactive = Hotel.GetInstance().Prices.Find(p => p.Id == price.Id);
            makePriceInactive.IsActive = false;
            priceRepository.Delete(price.Id);
        }
    }
}

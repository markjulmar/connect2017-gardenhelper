using GardenHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GardenHelper.Services.Mocks
{
    public class MockPricingDetails : IPricingDetails
    {
        public double ReturnedPrice { get; set; }

        public MockPricingDetails()
        {
            ReturnedPrice = new Random().NextDouble();
        }

        public Task<double> GetPriceFromId(string id)
        {
            return Task.FromResult(ReturnedPrice);
        }
    }
}

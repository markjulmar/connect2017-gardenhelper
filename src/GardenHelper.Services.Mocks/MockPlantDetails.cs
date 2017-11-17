using GardenHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GardenHelper.Models;
using System.Threading.Tasks;

namespace GardenHelper.Services.Mocks
{
    public class MockPlantDetails : IPlantDetails
    {
        public Task<Plant> GetPlantFromIdAsync(string id)
        {
            var plant = new Plant()
            {
                Id = id,
                Name = id.Length <= 1 ? id : Char.ToUpper(id[0]).ToString() + id.Substring(1),
                Description = "Lorum Ipsum",
                License = "This description is a mock and has no license associated with it.",
                LicenseUrl = "https://university.xamarin.com"
            };

            return Task.FromResult(plant);
        }
    }
}

using GardenHelper.Models;
using System.Threading.Tasks;

namespace GardenHelper.Interfaces
{

    public interface IPlantDetails
    {
        Task<Plant> GetPlantFromIdAsync(string id);
    }
}

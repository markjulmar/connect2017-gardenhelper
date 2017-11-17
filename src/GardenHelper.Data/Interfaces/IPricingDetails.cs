using System.Threading.Tasks;

namespace GardenHelper.Interfaces
{
    public interface IPricingDetails
    {
        Task<double> GetPriceFromId(string id);
    }
}

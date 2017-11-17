using GardenHelper.Interfaces;
using GardenHelper.Models.Internal;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GardenHelper.Services.Azure
{
    public class AzurePricingService : IPricingDetails
    {
        DateTime? lastUpdated;
        List<PriceEntry> prices;

        public async Task<double> GetPriceFromId(string id)
        {
            PriceEntry price;

            id = id?.Trim();
            Func<PriceEntry,bool> comparer = pe => string.Compare(pe.Item.Trim(), id, true) == 0;

            if (lastUpdated != null && (DateTime.Now - lastUpdated.Value).Minutes < 5)
            {
                price = prices?.FirstOrDefault(comparer);
                if (price != null)
                    return price.Price;
            }

            lastUpdated = DateTime.Now;

            // Not found - refresh the collection.
            using (var client = new MobileServiceClient(AuthKeys.GardenCenterMobileUrl))
            {
                prices = await client.GetTable<PriceEntry>().ToListAsync().ConfigureAwait(false);
            }

            price = prices.FirstOrDefault(comparer);
            return (price != null) ? price.Price : 0;
        }
    }
}

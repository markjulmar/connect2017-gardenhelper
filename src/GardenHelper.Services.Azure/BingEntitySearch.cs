using GardenHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GardenHelper.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using GardenHelper.Models.Internal;
using System.Linq;

namespace GardenHelper.Services.Azure
{
    public class BingEntitySearch : IPlantDetails
    {
        public async Task<Plant> GetPlantFromIdAsync(string id)
        {
            // Unfortunately, the ProjectOxford lib doesn't seem to work properly; 
            // it's not deserializing the data correctly so we will hit the API endpoint
            // directly to pull our search results.

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AuthKeys.EntityApiKey);
                string result = await client.GetStringAsync(string.Format(AuthKeys.BingEntitiesUrl, Uri.EscapeUriString(id)));
                var response = JsonConvert.DeserializeObject<SearchResponse>(result);
                var entities = response.Entities.Value;

                // Look for dominent entry first.
                var primaryEntity = entities.FirstOrDefault(
                    e => e.EntityPresentationInfo.EntityScenario == EntityPresentationInfo.DominantEntity);
                // Not found. Look for "generic" entities (not organizations, universities, etc.)
                if (primaryEntity == null)
                    primaryEntity = entities.FirstOrDefault(e => e.EntityPresentationInfo.EntityTypeHints.Contains("Generic"));

                if (primaryEntity != null)
                {
                    return new Plant
                    {
                        Id = id,
                        Name = primaryEntity.Name,
                        Description = primaryEntity.Description,
                        License = primaryEntity.ContractualRules.FirstOrDefault(cr => cr.Type == "ContractualRules/LicenseAttribution")?.LicenseNotice,
                        LicenseUrl = primaryEntity.ContractualRules.FirstOrDefault(cr => cr.Type == "ContractualRules/LicenseAttribution")?.License?.Url
                    };
                }
            }

            return null;
        }
    }
}

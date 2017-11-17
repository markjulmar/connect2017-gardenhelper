using Newtonsoft.Json;

namespace GardenHelper.Models.Internal
{
    [JsonObject(Title = "prices")]
    internal class PriceEntry
    {
        public string Id { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
    }
}

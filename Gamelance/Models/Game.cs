using Newtonsoft.Json;

namespace Gamelance.Models
{
    public class Game
    {
        public long GameId { get; set; } 

        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();
    }
}

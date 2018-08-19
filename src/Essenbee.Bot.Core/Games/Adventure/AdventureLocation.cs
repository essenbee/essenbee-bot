using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureLocation
    {
        public string LocationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IList<AdventureItem> Items { get; set; }
        public IDictionary<string, string> Moves { get; set; }

        public AdventureLocation()
        {
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string>();
        }
    }
}

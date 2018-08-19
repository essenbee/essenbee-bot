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

        public AdventureLocation()
        {

        }
    }
}

using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games
{
    public class AdventureLocation
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IList<AdventureItem> Items { get; set; }

        public AdventureLocation()
        {

        }
    }
}

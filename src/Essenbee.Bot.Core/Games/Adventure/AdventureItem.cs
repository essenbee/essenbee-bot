using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureItem
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public IList<AdventureItem> Contents { get; set; }

        public AdventureItem()
        {
            Contents = new List<AdventureItem>();
        }
    }
}

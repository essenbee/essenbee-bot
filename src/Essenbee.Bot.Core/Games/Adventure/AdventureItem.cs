using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureItem
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public bool IsContainer { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public string ItemIdToUnlock { get; set; }
        public bool IsPortable { get; set; }
        public bool IsEndlessSupply { get; set; }
        public IList<AdventureItem> Contents { get; set; }

        public AdventureItem()
        {
            Contents = new List<AdventureItem>();
        }
    }
}

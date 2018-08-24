using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureItem
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool IsContainer { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public string ItemIdToUnlock { get; set; }
        public bool IsPortable { get; set; }
        public bool IsEndlessSupply { get; set; }
        public IList<AdventureItem> Contents { get; set; }
        public IDictionary<string, Action<AdventurePlayer>> Interactions { get; set; }

        public AdventureItem()
        {
            Contents = new List<AdventureItem>();
            Interactions = new Dictionary<string, Action<AdventurePlayer>>();
        }

        public void AddInteraction(Dictionary<string, Action<AdventurePlayer>> interaction)
        {
            interaction.ToList().ForEach(x => Interactions.Add(x.Key, x.Value));
        }

        public void RemoveInteraction(string interactionKey)
        {
            Interactions.Remove(interactionKey);
        }
    }
}

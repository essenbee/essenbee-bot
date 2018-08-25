using Essenbee.Bot.Core.Games.Adventure.Interactions;
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
        public IDictionary<string, IAction> Interactions { get; set; }
        public IDictionary<string, string> InteractionAlias { get; set; }

        public AdventureItem()
        {
            Contents = new List<AdventureItem>();
            Interactions = new Dictionary<string, IAction>();
        }

        public void Interact(string requestedAction, AdventurePlayer player)
        {
            requestedAction = requestedAction.ToLower();

            foreach (var action in Interactions.OrderBy(i => i.Key))
            {
                var keyword = action.Key.Split('-')[0];

                if (keyword.Equals(requestedAction))
                {
                    action.Value.Do(player, this);
                }
            }
        }

        public void AddInteraction(string keyword, IAction value)
        {
            // Add numeric suffix to key
            var n = Interactions.Keys.Where(k => k.StartsWith(keyword)).Count();
            var key = keyword + $"-{++n}";
            Interactions.Add(key, value);
        }

        public void RemoveInteractions(string interactionKey)
        {
            foreach (var action in Interactions.Where(i => i.Key.StartsWith(interactionKey)))
            {
                Interactions.Remove(action.Key);
            }
        }
    }
}

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
        public IList<IInteraction> Interactions { get; set; }

        public AdventureItem()
        {
            Contents = new List<AdventureItem>();
            Interactions = new List<IInteraction>();
        }

        public bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb));

            if (interaction != null)
            {
                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, this);
                }

                return true;
            }

            return false;
        }
    }
}

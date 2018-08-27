using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public abstract class AdventureItem
    {
        public Guid UniqueId { get; }
        public Item ItemId { get; set; }
        public List<string> Nouns { get; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool IsContainer { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public Item ItemIdToUnlock { get; set; }
        public bool IsPortable { get; set; }
        public bool IsEndlessSupply { get; set; }
        public IList<AdventureItem> Contents { get; set; }
        public IList<IInteraction> Interactions { get; set; }
        public IReadonlyAdventureGame Game { get; }

        public AdventureItem(IReadonlyAdventureGame game, params string[] nouns)
        {
            UniqueId = Guid.NewGuid();
            Game = game;
            Contents = new List<AdventureItem>();
            Interactions = new List<IInteraction>();
            ItemIdToUnlock = Item.Unknown;
            Nouns = nouns.ToList();
        }

        public bool IsMatch(string noun) => Nouns.Any(v => noun.Equals(v));

        public virtual bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

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

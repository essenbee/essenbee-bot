using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public abstract class AdventureItem : IAdventureItem
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
        public Item ItemIdToUnlock { get; set; } = Item.None;
        public Item MustBeContainedIn { get; set; } = Item.None;
        public bool IsPortable { get; set; }
        public bool IsEndlessSupply { get; set; }
        public bool IsTransparent { get; set; }
        public bool IsTreasure { get; set; }
        public IList<IAdventureItem> Contents { get; set; }
        public IList<IInteraction> Interactions { get; set; }
        public IReadonlyAdventureGame Game { get; }
        public Item PreventTakeItemId { get; set; } = Item.None;
        public string PreventTakeText { get; set; } = "";
        public Dictionary<string, List<string>> PlayerItemState { get; set; } = new Dictionary<string, List<string>>();

        protected AdventureItem(IReadonlyAdventureGame game, params string[] nouns)
        {
            UniqueId = Guid.NewGuid();
            Game = game;
            Contents = new List<IAdventureItem>();
            Interactions = new List<IInteraction>();
            Nouns = nouns.ToList();
        }

        public bool IsMatch(string noun) => Nouns.Any(v => noun.Equals(v));

        public bool ContainerRequired() => MustBeContainedIn != Item.None;

        public virtual bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                foreach (var action in interaction.RegisteredInteractions)
                {
                    if (!action.Do(player, this)) break;
                }

                return true;
            }

            return false;
        }

        public bool HasState(IAdventurePlayer player, string state)
        {
            if (PlayerItemState.ContainsKey(player.Id))
            {
                var states = PlayerItemState[player.Id];

                if (states.Contains(state))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

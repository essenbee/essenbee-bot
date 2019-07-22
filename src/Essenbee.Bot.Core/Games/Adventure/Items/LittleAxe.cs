using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class LittleAxe : AdventureItem
    {
        internal LittleAxe(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.LittleAxe;
            Name = "a small *axe*";
            PluralName = "small *axes*";
            IsPortable = true;
            IsWeapon = true;
            
            var use = new ItemInteraction(Game, "use", "swing", "throw", "wield");
            use.RegisteredInteractions.Add(new Display("You attack the dwarf, throwing the little axe at it!!"));
            use.RegisteredInteractions.Add(new RemoveFromInventory());
            use.RegisteredInteractions.Add(new Chance(80));

            Interactions.Add(use);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            var retVal = false;
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                var dwarf = GetDwarfIfPresent(player);

                if (dwarf != null)
                {
                    var killed = false;

                    foreach (var action in interaction.RegisteredInteractions)
                    {
                        killed = action.Do(player, this);
                    }

                    if (killed)
                    {
                        Game.Dungeon.TryGetLocation(Location.Nowhere, out var nowhere);
                        dwarf.CurrentLocation = nowhere;

                        if (player.EventRecord.ContainsKey(EventIds.Dwarves))
                        {
                            player.EventRecord[EventIds.Dwarves]++;
                        }

                        player.ChatClient.PostDirectMessage(player, "Your aim is true and the dwarf falls dead!" +
                            " The body disappears in a cloud of greasy smoke...");
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player, "The dwarf dodged out of harms way!");
                    }

                    retVal = true;
                }
                else
                {
                    player.ChatClient.PostDirectMessage(player,
                        "The axe doesn't seem to be of much use in this particular situation!");
                }

                var addItem = new AddToLocation(this);
                addItem.Do(player, null);
            }

            return retVal;
        }

        private WanderingMonster GetDwarfIfPresent(IAdventurePlayer player)
        {
            var manager = Game.MonsterManagers.FirstOrDefault();

            if (manager is null)
            {
                return null;
            }

            return manager.Monsters.FirstOrDefault(d => d.CurrentLocation != null &&
             d.CurrentLocation.LocationId.Equals(player.CurrentLocation.LocationId));
        }
    }
}

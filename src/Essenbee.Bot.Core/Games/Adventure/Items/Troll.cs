using Essenbee.Bot.Core.Games.Adventure.Commands;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Troll : AdventureItem
    {
        private static Troll _instance = null;
        private static object _mutex = new object();

        internal Troll(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Troll;
            Name = "large troll barring your way";
            PluralName = "large trolls barring your way";
            IsCreature = true;

            var kill = new ItemInteraction(Game, "kill", "slay", "murder", "attack");
            Interactions.Add(kill);
        }

        public static Troll GetInstance(IReadonlyAdventureGame game, params string[] nouns)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new Troll(game, nouns);
                    }
                }
            }

            return _instance;
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("kill"))
                {

                }

                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, this);
                }

                return true;
            }

            return false;
        }

        public override void Attack(IAdventurePlayer player, IAdventureItem troll)
        {
            player.ChatClient.PostDirectMessage(player, "You try to to attack the troll, but its foul breath drives you back!");
        }

        public override void Give(IAdventurePlayer player, IAdventureItem item, IAdventureItem troll)
        {
            // Destroy item
            var removeItem = new RemoveFromInventory();
            removeItem.Do(player, item);

            if (item.IsTreasure)
            {
                // Place item in the Troll's Cave
                Game.Dungeon.TryGetLocation(Location.TrollCave, out var location);

                var addToLocation = new AddToLocation(item, location);
                addToLocation.Do(player, item);

                // Troll will move away from the bridge
                player.ChatClient.PostDirectMessage(player,
                    $"The troll accepts your {item.Name}, appraises it with a critical eye, and retreats under its bridge!");
            }
            else
            {
                // The troll throws the item into the chasm
                player.ChatClient.PostDirectMessage(player,
                    $"The troll scowls at you and throws the {item.Name} into the chasm. 'Pay Troll!', it roars.");

                return;
            }

            Game.Dungeon.TryGetLocation(Location.SouthWestOfChasm, out var swOfChasm);
            Game.Dungeon.TryGetLocation(Location.NorthEastOfChasm, out var neOfChasm);
            RemoveTroll(player, troll, swOfChasm, neOfChasm);

            var clock = new StartClock("troll");
            clock.Do(player, troll);
        }

        private void RemoveTroll(IAdventurePlayer player, IAdventureItem troll, IAdventureLocation swOfChasm, IAdventureLocation neOfChasm)
        {
            var removeTroll1 = new RemoveFromLocation(troll, swOfChasm);
            var removeTroll2 = new RemoveFromLocation(troll, neOfChasm);
            removeTroll1.Do(player, troll);
            removeTroll2.Do(player, troll);

            AllowPassage(player);
        }

        private void AllowPassage(IAdventurePlayer player)
        {
            var addMove1 = new AddMoves(new List<IPlayerMove>
                {
                            new PlayerMove(string.Empty, Location.TrollBridge, "northeast", "ne"),
                        }, Game, Location.SouthWestOfChasm);
            addMove1.Do(player, null);

            var addMove2 = new AddMoves(new List<IPlayerMove>
                {
                            new PlayerMove(string.Empty, Location.TrollBridge, "southwest", "sw"),
                        }, Game, Location.NorthEastOfChasm);
            addMove2.Do(player, null);
        }
    }
}

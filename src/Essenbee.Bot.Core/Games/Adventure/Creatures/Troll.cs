using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Creatures
{
    public class Troll : AdventureCreature
    {
        private static Troll _instance = null;
        private static object _mutex = new object();

        internal Troll(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Troll;
            Name = "large troll barring your way";
            PluralName = "large trolls barring your way";

            // Player says yes to attacking with bare hands...
            var yes = new ItemInteraction(Game, "yes");
            yes.RegisteredInteractions.Add(new RemovePlayerItemState("attack"));
            yes.RegisteredInteractions.Add(new Display("As you approach the hideous troll, it points at its sign and yells 'Pay troll!'"));
            yes.RegisteredInteractions.Add(new Display("The creature's foul breath drives you back, gasping for air."));
            Interactions.Add(yes);
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
                if ((interaction.Verbs.Contains("yes") || interaction.Verbs.Contains("no"))
                    && !HasState(player, "attack"))
                {
                    return false;
                }

                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, this);
                }

                return true;
            }

            return false;
        }

        public override void Attack(IAdventurePlayer player, IAdventureItem troll, IAdventureItem weapon = null)
        {
            if (weapon != null)
            {
                player.ChatClient.PostDirectMessage(player, $"You try to to attack the troll, but it snatches the {weapon.Name} from your puny grasp and casts it into the chasm!");
                var loseWeapon = new RemoveFromInventory();
                loseWeapon.Do(player, weapon);
            }
            else
            {
                player.ChatClient.PostDirectMessage(player, $"As you approach the hideous troll, it points at its sign and yells 'Pay troll!'");
                player.ChatClient.PostDirectMessage(player, $"The creature's foul breath drives you back, gasping for air.");

            }
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

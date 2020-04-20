using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Creatures
{
    public class Snake : AdventureCreature
    {
        internal Snake(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Snake;
            Name = "large vicious-looking snake barring your way";
            PluralName = "large vicious-looking snakes barring your way";

            // Player says yes to attacking with bare hands...
            var yes = new ItemInteraction(Game, "yes");
            yes.RegisteredInteractions.Add(new RemovePlayerItemState("attack"));
            yes.RegisteredInteractions.Add(new Display("You lunge at the snake, trying to grapple with it..."));
            yes.RegisteredInteractions.Add(new Display("The snake sinks its envenomed fangs into your softly yielding flesh"));
            yes.RegisteredInteractions.Add(new PlayerDies(game));
            Interactions.Add(yes);
        }

        public override void Attack(IAdventurePlayer player, IAdventureItem troll, IAdventureItem weapon)
        {
            player.ChatClient.PostDirectMessage(player, "The snake hisses horribly and strikes it you!");

            if (weapon != null)
            {
                player.ChatClient.PostDirectMessage(player, $"You are so startled that you drop your {weapon.Name}!");

                var removeItem = new RemoveFromInventory();
                removeItem.Do(player, weapon);

                // Place item in the Hall of the Mountain King
                Game.Dungeon.TryGetLocation(Location.HallOfMountainKing, out var location);
                var addToLocation = new AddToLocation(weapon, location);
                addToLocation.Do(player, weapon);
            }

            return;
        }

        public override void Give(IAdventurePlayer player, IAdventureItem item, IAdventureItem snake)
        {
            var removeItem = new RemoveFromInventory();
            removeItem.Do(player, item);

            // Place item in the Hall of the Mountain King
            Game.Dungeon.TryGetLocation(Location.HallOfMountainKing, out var location);
            var addToLocation = new AddToLocation(item, location);
            addToLocation.Do(player, item);

            player.ChatClient.PostDirectMessage(player, $"The snake hisses and snaps at you! It ignores the {item.Name}");
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
    }
}

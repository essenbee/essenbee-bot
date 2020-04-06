using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Snake : AdventureItem
    {
        internal Snake(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Snake;
            Name = "large vicious-looking snake barring your way";
            PluralName = "large vicious-looking snakes barring your way";
            IsCreature = true;
        }

        public override void Attack(IAdventurePlayer player, IAdventureItem troll)
        {
            player.ChatClient.PostDirectMessage(player, "The snake hisses and snaps at you! You cannot get near enough to it to strike!");
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
    }
}

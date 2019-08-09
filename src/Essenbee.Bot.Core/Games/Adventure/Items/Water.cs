using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Water : AdventureItem
    {
        internal Water(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.PintOfWater;
            Name = "source of *water*";
            PluralName = "source of *water*";
            IsPortable = false;
            MustBeContainedIn = Item.None;
            Slots = 0;

            var drink = new ItemInteraction(Game, "drink");
            drink.RegisteredInteractions.Add(new Display("Mmmmm, refreshing!"));
            Interactions.Add(drink);
        }
    }
}

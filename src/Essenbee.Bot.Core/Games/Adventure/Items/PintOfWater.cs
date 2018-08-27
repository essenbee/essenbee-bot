using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class PintOfWater : AdventureItem
    {
        internal PintOfWater(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.PintOfWater;
            Name = "pint of *water*";
            PluralName = "pin of *water*";
            IsPortable = false;
            IsEndlessSupply = true;
        }
    }
}

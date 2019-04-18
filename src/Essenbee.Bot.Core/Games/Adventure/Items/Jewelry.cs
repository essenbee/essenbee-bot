using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Jewelry : AdventureItem
    {
        internal Jewelry(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Jewelry;
            Name = "piece of *jewellery*";
            PluralName = "pieces of *jewellery*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

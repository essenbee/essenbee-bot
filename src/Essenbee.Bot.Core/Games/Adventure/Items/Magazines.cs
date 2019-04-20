using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Magazines : AdventureItem
    {
        internal Magazines(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Magazines;
            Name = "a few old copies of 'Spelunker Today' *magazine*";
            PluralName = "a few old copies of 'Spelunker Today' *magazine*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

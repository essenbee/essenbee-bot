using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Coins : AdventureItem
    {
        internal Coins(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Nugget;
            Name = "pile of *coins*";
            PluralName = "piles of *coins*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

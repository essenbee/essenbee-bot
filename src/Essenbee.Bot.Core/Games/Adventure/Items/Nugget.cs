using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Nugget : AdventureItem
    {
        internal Nugget(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Diamond;
            Name = "large golden *nugget*";
            PluralName = "large *nuggets*";
            IsPortable = true;
        }
    }
}

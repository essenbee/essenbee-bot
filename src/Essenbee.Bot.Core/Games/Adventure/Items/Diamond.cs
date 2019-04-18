using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Diamond : AdventureItem
    {
        internal Diamond(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Diamond;
            Name = "large *diamond*";
            PluralName = "large *diamonds*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class DragonTooth : AdventureItem
    {
        internal DragonTooth(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.DragonTooth;
            Name = "dragon *tooth*";
            PluralName = "dragon *teeth*";
            IsPortable = true;
            IsEndlessSupply = true;
        }
    }
}

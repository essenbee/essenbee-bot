using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class SpentBatteries : AdventureItem
    {
        internal SpentBatteries(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Batteries;
            Name = "couple of spent *batteries*";
            PluralName = "several of spent *batteries*";
            IsPortable = true;
            IsEndlessSupply = false;
        }
    }
}


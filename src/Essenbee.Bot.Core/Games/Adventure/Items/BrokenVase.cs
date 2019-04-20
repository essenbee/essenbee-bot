using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class BrokenVase : AdventureItem
    {
        internal BrokenVase(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.BrokenVase;
            Name = "spread of worthless broken pottery";
            PluralName = "spread of worthless broken pottery";
            IsPortable = false;
        }
    }
}

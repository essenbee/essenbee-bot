using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Jewellry : AdventureItem
    {
        internal Jewellry(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.BarsOfSilver;
            Name = "piece of *jewellry*";
            PluralName = "pieces of *jewellry*";
            IsPortable = true;
        }
    }
}

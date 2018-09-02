using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class BarsOfSilver : AdventureItem
    {
        internal BarsOfSilver(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.BarsOfSilver;
            Name = "few bars of *silver*";
            PluralName = "few bars of *silver*";
            IsPortable = true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class PlatinumPyramid : AdventureItem
    {
        internal PlatinumPyramid(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.PlatinumPyramid;
            Name = "platinum *pyramid*";
            PluralName = "platinum *pyramids*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

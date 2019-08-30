using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Emerald : AdventureItem
    {
        internal Emerald(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Emerald;
            Name = "shining *emerald*";
            PluralName = "shining *emeralds*";
            IsPortable = true;
            IsTreasure = true;
        }
    }
}

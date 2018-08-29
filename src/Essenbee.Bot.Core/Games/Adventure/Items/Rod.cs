using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Rod : AdventureItem
    {
        internal Rod(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Rod;
            Name = "black *rod* with a rusty star on one end";
            PluralName = "black *rods* with rusty stars on the ends";
            IsActive = true;
            Contents = new List<AdventureItem>();
            IsPortable = true;
        }
    }
}

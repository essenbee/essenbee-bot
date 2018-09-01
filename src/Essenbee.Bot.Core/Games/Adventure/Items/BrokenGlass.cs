using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class BrokenGlass : AdventureItem
    {
        internal BrokenGlass(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.BrokenGlass;
            Name = "spread of broken glass";
            PluralName = "spread of broken glass";
            IsPortable = false;
            IsContainer = true;
            IsOpen = true;
            Contents = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.ShardOfGlass) };
        }
    }
}

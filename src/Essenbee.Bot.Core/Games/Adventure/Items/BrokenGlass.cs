using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class BrokenGlass : AdventureItem
    {
        internal BrokenGlass(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "glass";
            Name = "spread of broken glass";
            PluralName = "spread of broken glass";
            IsPortable = false;
            IsContainer = true;
            IsOpen = true;
            Contents = new List<AdventureItem> { new ShardOfGlass(Game) };
        }
    }
}

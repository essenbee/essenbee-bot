using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class BrokenGlass : AdventureItem
    {
        public IReadonlyAdventureGame Game { get; }

        public BrokenGlass(IReadonlyAdventureGame game)
        {
            Game = game;
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

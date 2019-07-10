using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations.AllAlikeMaze
{
    public class PirateChestCave : AdventureLocation
    {
        public PirateChestCave(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.PirateChestCave;
            Name = "Pirate Chest";
            ShortDescription = "in a tiny chamber";
            LongDescription = "in a tiny, rough-hewn chamber. It seems that this space has been cleared out of debris fairly recently...";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.PirateChest) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike14, "southeast", "se"),
            };
        }
    }
}

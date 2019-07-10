using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike14 : AdventureLocation
    {
        public AllAlike14(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike14;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.BrinkOfPit, "north", "n"),
                new PlayerMove("", Location.AllAlike13, "west", "w"),
                new PlayerMove("", Location.PirateChestCave, "northwest", "nw"),
            };
        }
    }
}

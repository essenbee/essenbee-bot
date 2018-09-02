using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations.AllAlikeMaze
{
    public class BrinkOfPit : AdventureLocation
    {
        public BrinkOfPit(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.BrinkOfPit;
            Name = "Brink of Pit";
            ShortDescription = "on the brink of a pit";
            LongDescription = "standing on the brink of a pit. Tunnels lead off tot he north, south, east and west.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You enter the pit, and squeeze yourself through an impossibly narrow crack...",
                    Location.BirdChamber, "down"),
                new PlayerMove("", Location.AllAlike13, "north", "n"),
                // Dead end? new PlayerMove("", Location.AllAlike5, "south", "s"),
                new PlayerMove("", Location.AllAlike14, "east", "e"),
                new PlayerMove("", Location.AllAlike12, "west", "w"),
            };
        }
    }
}

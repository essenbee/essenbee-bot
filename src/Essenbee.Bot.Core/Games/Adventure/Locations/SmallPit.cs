using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SmallPit : AdventureLocation
    {
        public SmallPit(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SmallPit;
            Name = "Top of Small Pit";
            ShortDescription = "at the top of a small pit";
            LongDescription = "at the top of a small pit. Wisps of white mist emerge from its mouth. An eastern passage ends here, except for a small crack leading on.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.BirdChamber, "east", "e"),
                new PlayerMove("You carefully climb down into the misty pit. At the bottom, you follow a low, winding passageway...", 
                    Location.HallOfMistsEast, "down", "d"),
            };
        }
    }
}

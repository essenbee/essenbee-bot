using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class BirdChamber : AdventureLocation
    {
        public BirdChamber(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.BirdChamber;
            Name = "Bird Chamber";
            ShortDescription = "in bird chamber";
            LongDescription = "You are in a splendid chamber thirty feet high. The walls are frozen rivers of orange flowstone." + 
                              " An awkward canyon and a good passage exit from east and west sides of the chamber.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Canyon, "east", "e"),
                new PlayerMove(Location.SmallPit, "west", "w"),
            };
        }
    }
}

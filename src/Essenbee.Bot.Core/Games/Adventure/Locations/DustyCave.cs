using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class DustyCave : AdventureLocation
    {
        public DustyCave(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.DustyCave;
            Name = "dusty rock room";
            ShortDescription = "in the dusty rock room.";
            LongDescription = "in a large room full of dusty rocks. There is a big hole in " +
                "the floor. There are cracks everywhere, and a passage leading east.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.ComplexJunction, "down", "d", "hole"),
                //new PlayerMove(string.Empty, Location., "east", "e", "passage"), // dirty broken passage
                new PlayerMove(string.Empty, Location.Bedquilt, "bedquilt"),
            };
        }
    }
}

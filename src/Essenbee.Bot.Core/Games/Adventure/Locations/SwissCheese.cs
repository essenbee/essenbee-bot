using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SwissCheese : AdventureLocation
    {
        public SwissCheese(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SwissCheese;
            Name = "Swiss Cheese";
            ShortDescription = "in the Swiss Cheese room";
            LongDescription = "in a room whose walls resemble Swiss cheese. Obvious passages " +
                "head west, east, NE, and NW. Part of the room is occupied by a large bedrock block that " +
                "is full of small holes, just like a Swiss cheese.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                //new PlayerMove(string.Empty, Location., "west", "w"), // east-end 2 pit room
                new PlayerMove(string.Empty, Location.SoftRoom, "east", "e"),
                new PlayerMove(string.Empty, Location.Bedquilt, "northeast", "ne", "bedquilt"),
                new PlayerMove(string.Empty, Location.TallEastWestCanyon, "canyon"),
                new PlayerMove(string.Empty, Location.Oriental, "oriental"),
                new RandomMove(string.Empty, new List<Location>
                {
                    Location.TallEastWestCanyon, Location.Oriental, Location.Oriental,
                }, "south", "s", "northwest", "nw"),
            };
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    class Bedquilt : AdventureLocation
    {
        public Bedquilt(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Bedquilt;
            Name = "Bedquilt Cave";
            ShortDescription = "back at Bedquilt cave";
            LongDescription = "in Bedquilt cave, a long East/West passage with holes everywhere. " +
                "To explore at random select NORTH, SOUTH, UP, or DOWN.";

            Items = new List<IAdventureItem>();
            Level = 1;

            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.SlabRoom, "slab"),
                new PlayerMove(string.Empty, Location.SwissCheese, "west", "w"),
                new PlayerMove(string.Empty, Location.ComplexJunction, "east", "e"),
                new RandomMove("You have crawled around in some little holes and found your way to:", 
                new List<Location> 
                {
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.LowRoom,
                    Location.SecretJunction, Location.Bedquilt, Location.Bedquilt, Location.LowRoom,
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.LowRoom,
                }, "north", "n"),
                new RandomMove("You have crawled around in some little holes and found your way to:", 
                new List<Location>
                {
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.SlabRoom,
                    Location.TallEastWestCanyon, Location.Bedquilt, Location.Bedquilt, Location.SlabRoom,
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.SlabRoom,
                }, "south", "s"),
                new RandomMove("You have crawled around in some little holes and found your way to:", 
                new List<Location>
                {
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.DustyCave,
                    Location.SecretNorthSouthPassage, Location.Bedquilt, Location.Bedquilt, Location.DustyCave,
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.DustyCave,
                }, "up", "u", "climb"),
                new RandomMove("You have crawled around in some little holes and found your way to:", 
                new List<Location>
                {
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.Anteroom,
                    Location.Anteroom, Location.Bedquilt, Location.Bedquilt, Location.Anteroom,
                    Location.Bedquilt, Location.Bedquilt, Location.Bedquilt, Location.Anteroom,
                }, "down", "d"),
            };

            MonsterValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.SlabRoom, "slab"),
                new PlayerMove(string.Empty, Location.SwissCheese, "west", "w"),
                new PlayerMove(string.Empty, Location.ComplexJunction, "east", "e"),
                new PlayerMove(string.Empty, Location.Anteroom, "ante"),
                new PlayerMove(string.Empty, Location.TallEastWestCanyon, "tall"),
                new PlayerMove(string.Empty, Location.SecretJunction, "secret"),
            };
        }
    }
}

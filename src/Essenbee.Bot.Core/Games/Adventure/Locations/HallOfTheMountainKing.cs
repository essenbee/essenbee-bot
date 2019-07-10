using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class HallOfTheMountainKing : AdventureLocation
    {
        public HallOfTheMountainKing(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.HallOfMountainKing;
            Name = "Hall of the Mountain Kings";
            ShortDescription = "in Hall of the Mountain King";
            LongDescription = "in the Hall of the Mountain King, with passages off in all directions. Sounds echo in this vast, high space.";
            Level = 1;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Snake) };
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("A vicious-looking snake bars your way!", Location.HallOfMountainKing, "south", "s", "north", "n", "west", "w", "down", "d"),
                new PlayerMove("You ascend a majestic staircase of natural rock...", Location.HallOfMistsEast, "up", "east", "e"),
            };

            MonsterValidMoves = new List<IPlayerMove> 
            {
                new PlayerMove(string.Empty, Location.LowPassage, "north", "n"),
                new PlayerMove(string.Empty, Location.SouthSideChamber, "south", "s"),
                new PlayerMove(string.Empty, Location.WestSideChamber, "west", "w"),
                new PlayerMove(string.Empty, Location.SecretEastWestCanyon, "secret"),
                new PlayerMove(string.Empty, Location.HallOfMistsEast, "up", "east", "e"),
            };
        }
    }
}

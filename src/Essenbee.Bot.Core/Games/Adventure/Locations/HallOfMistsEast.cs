using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class HallOfMistsEast : AdventureLocation
    {
        public HallOfMistsEast(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.HallOfMistsEast;
            Name = "Hall of Mists";
            ShortDescription = "in hall of mists";
            LongDescription = "at one end of a vast hall stretching forward out of sight to the west. There are openings " +
                "to either side. Nearby, a wide, natural stone staircase leads downward. The hall is filled with wisps of white " +
                "mist swaying to and fro, almost as if alive. A cold wind blows up the staircase. There is a passage at the top" +
                "of a dome behind you.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("You follow a low, winding passage to the base of a pit, then struggle upwards...", 
                    Location.SmallPit, "up", "e", "east", "u", "climb"),
                new PlayerMove("You descend a majestic staircase of natural rock, worn smooth by water flowing in the past...",
                    Location.HallOfMountainKing, "north", "n", "down", "d"),
                new PlayerMove("The roof of the passage becomes so low, you have to crawl...",
                    Location.GoldRoom, "south", "s"),
                new PlayerMove("You make your way deeper into the mists...",
                    Location.FissureEast, "west", "w"),
            };
        }
    }
}

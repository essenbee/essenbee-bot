using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Crossover : AdventureLocation
    {
        public Crossover(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Crossover;
            Name = "Crossover";
            ShortDescription = "at a crossover of a high N/S passage and a low E/W one";
            LongDescription = "at a crossover of a high N/S passage and a low E/W one";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.DeadEndWithMessage, "north", "n"),
                new PlayerMove(string.Empty, Location.LongHallWest, "south", "s"),
                new PlayerMove("The passage twists and descends. Water drips from the ceiling.", 
                    Location.WestSideChamber, "east", "e", "down", "d"),
                new PlayerMove(string.Empty, Location.LongHallEast, "west", "w"),
            };
        }
    }
}

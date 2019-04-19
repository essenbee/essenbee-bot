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

            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                //new PlayerMove(string.Empty, Location., "north", "n"), // dead end
                //new PlayerMove(string.Empty, Location., "south", "s"), // long hall west
                new PlayerMove("The passage twists and descends. Water drips from the ceiling.", 
                    Location.WestSideChamber, "east", "e"),
                //new PlayerMove(string.Empty, Location., "west", "w"), // long hall east
            };
        }
    }
}

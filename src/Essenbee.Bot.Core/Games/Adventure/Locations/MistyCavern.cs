using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class MistyCavern : AdventureLocation
    {
        public MistyCavern(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.MistyCavern;
            Name = "Misty Cavern";
            ShortDescription = "in misty cavern";
            LongDescription = "following a wide path around the outer edge of a large cavern. " +
                              "Far below, through a heavy white mist, strange splashing noises can be " +
                              "heard. The mist rises up through a fissure in the ceiling. The path " +
                              "exits to the south and west.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.Oriental, "south", "s"),
                new PlayerMove(string.Empty, Location.Alcove, "west", "w"),
            };
        }
    }
}

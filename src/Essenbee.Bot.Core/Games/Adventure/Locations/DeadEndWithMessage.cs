using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class DeadEndWithMessage : AdventureLocation
    {
        public DeadEndWithMessage(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.DeadEndWithMessage;
            Name = "dead end";
            ShortDescription = "at a dead end";
            LongDescription = "at a dead end. Scratched on a rock is the message: 'Stand where " +
                "the statue gazes, and make use of the proper tool'.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.Crossover, "south", "s"),
            };
        }
    }
}

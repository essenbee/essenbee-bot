using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Alcove : AdventureLocation
    {
        public Alcove(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Alcove;
            Name = "Alcove";
            ShortDescription = "in an alcove";
            LongDescription = "in an alcove. A small NW path seems to widen after a short distance." +
                " An extremely tight tunnel leads east.  It looks like a very " +
                "tight squeeze. An eerie light can be seen at the other end.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new SqueezeMove("You breathe in and make yourself at thin as possible...", Location.PloverRoom, "east", "e"),
                new PlayerMove(string.Empty, Location.MistyCavern, "northwest", "nw"),
            };
        }
    }
}

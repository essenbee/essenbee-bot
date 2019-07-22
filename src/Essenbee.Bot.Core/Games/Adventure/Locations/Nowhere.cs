using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Nowhere : AdventureLocation
    {
        public Nowhere(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Nowhere;
            Name = "nowhere";
            ShortDescription = "";
            LongDescription = "";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {

            };
        }
    }
}

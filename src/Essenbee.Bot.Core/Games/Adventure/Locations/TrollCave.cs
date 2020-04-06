using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class TrollCave : AdventureLocation
    {
        public TrollCave(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.TrollCave;
            Name = "Troll Cave";
            ShortDescription = "in a dank cave that smells of Troll.";
            LongDescription = "in a dank cave that smells of Troll.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>();
        }
    }
}
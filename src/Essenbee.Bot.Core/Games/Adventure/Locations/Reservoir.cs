using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Reservoir : AdventureLocation
    {
        public Reservoir(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Reservoir;
            Name = "reservoir";
            ShortDescription = "the southern edge of a reservoir";
            LongDescription = "on the southern edge of a large underground reservoir. A " +
                "thick cloud of white mist fills the room, rising from the surface " +
                "of the water and drifting rapidly upwards.  The lake is fed by a " +
                "stream, which tumbles out of a hole in the wall about 10 feet overhead " +
                "and splashes noisily into the water near the reservoir's northern wall. " +
                "A dimly-seen passage exits through the northern wall, but you can't get " +
                "across the water to get to it. Another passage leads south from here.";
            WaterPresent = true;
            
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("There is no way you can swim across a subterranean lake!", 
                    Location.Reservoir, "north", "n"),
                new PlayerMove(string.Empty, Location.MirrorCanyon, "south", "s"),
            };
        }
    }
}

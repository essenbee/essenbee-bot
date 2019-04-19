using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class MirrorCanyon : AdventureLocation
    {
        public MirrorCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.MirrorCanyon;
            Name = "mirror canyon";
            ShortDescription = "in mirror canyon";
            LongDescription = "in a north/south canyon about 25 feet across. The floor is " +
                "covered by white mist seeping in from the north. The walls extend " +
                "upward for well over 100 feet. Suspended from some unseen point far " +
                "above you, an enormous two-sided mirror is hanging parallel to and " +
                "midway between the canyon walls. (The mirror is obviously provided " +
                "for the use of the dwarves, who as you know, are extremely vain.) A " +
                "small window can be seen in either wall, some fifty feet up.";

            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                //new PlayerMove(string.Empty, Location., "north", "n"),
                new PlayerMove(string.Empty, Location.SecretNorthSouthCanyon, "south", "s"),
            };
        }
    }
}

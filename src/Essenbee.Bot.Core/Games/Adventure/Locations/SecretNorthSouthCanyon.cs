using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretNorthSouthCanyon : AdventureLocation
    {

        public SecretNorthSouthCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretNorthSouthCanyon;
            Name = "secret canyon";
            ShortDescription = "in a secret canyon which exits to the north and south, above a large room";
            LongDescription = "in a secret canyon which exits to the north and south, above a large room";

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.MirrorCanyon, "north", "n"),
                new PlayerMove(string.Empty, Location.SecretNorthEastCanyon, "south", "s"),
                new PlayerMove("You clamber downwards, finding plenty of hand and foot holds...", 
                    Location.SlabRoom, "down", "d"),
            };
        }
    }
}

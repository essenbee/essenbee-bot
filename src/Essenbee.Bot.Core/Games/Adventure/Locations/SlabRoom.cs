using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SlabRoom : AdventureLocation
    {
        public SlabRoom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SlabRoom;
            Name = "slab room";
            ShortDescription = "slab room";
            LongDescription = "a large low circular chamber whose floor is an immense slab " +
                "fallen from the ceiling (Slab room). East and west there once were " +
                "large passages, but they are now filled with boulders. Low small " +
                "passages go north and south, and the south one quickly bends west " +
                "around the boulders.";

            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.Bedquilt, "north", "n"),
                //new PlayerMove(string.Empty, Location.WestTwoPit, "south", "s"),
                new PlayerMove("You make your way upwards, finding plenty of hand and foot holds...",
                    Location.SecretNorthSouthCanyon, "up", "u", "climb"),
            };
        }
    }
}

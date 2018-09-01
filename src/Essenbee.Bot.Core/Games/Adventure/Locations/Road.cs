using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Road : AdventureLocation
    {
        public Road(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Road;
            Name = "End of a Road";
            ShortDescription = "standing at the end of a road.";
            LongDescription = "standing at the end of a road before a small brick building. Around you is a forest.  A small stream flows out of the building and down a gully.";
            WaterPresent = true;
            Items = new List<IAdventureItem> 
            {
                ItemFactory.GetInstance(Game, Item.Mailbox),
                ItemFactory.GetInstance(Game, Item.Water)
            };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You enter the building.", Location.Building, "east", "e", "enter", "in", "inside", "building"),
                new PlayerMove("You follow the stream.", Location.Valley, "south", "s", "valley", "gully", "downstream"),
                new PlayerMove("You move along the road up a hill.", Location.Hill, "west", "w", "hill", "road", "up"),
                new PlayerMove("You head off into the woods...", Location.Forest, "north", "n", "forest"),
            };
        }
    }
}

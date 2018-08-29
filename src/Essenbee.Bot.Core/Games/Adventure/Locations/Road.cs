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
            Items = new List<AdventureItem> { ItemFactory.GetInstance(Game, Item.Mailbox) };
            ValidMoves = new List<PlayerMove> {
                new PlayerMove("You enter the building.", Location.Building, "east", "e", "enter", "in", "inside", "building"),
                new PlayerMove(string.Empty, Location.Valley, "south", "s", "valley"),
                new PlayerMove(string.Empty, Location.Hill, "west", "w", "hill", "road", "up"),
                new PlayerMove(string.Empty, Location.Forest, "north", "n", "forest"),
            };
        }
    }
}

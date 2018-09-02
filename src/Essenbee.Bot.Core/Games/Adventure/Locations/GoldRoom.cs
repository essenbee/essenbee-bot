using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class GoldRoom : AdventureLocation
    {
        public GoldRoom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.GoldRoom;
            Name = "Gold Room";
            ShortDescription = "in nugget of gold room";
            LongDescription = "in a low room with a crude note on the wall. The note says, `You won't get it up the steps`.";
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Nugget) };
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.HallOfMistsEast, "north", "n"),

            };
        }
    }
}

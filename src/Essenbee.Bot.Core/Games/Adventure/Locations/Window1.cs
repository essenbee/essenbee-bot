using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Window1 : AdventureLocation
    {
        public Window1(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Window1;
            Name = "window on pit";
            ShortDescription = "at window on pit";
            LongDescription = "at a low window overlooking a huge pit, which extends up out of sight. " + 
                "A floor is indistinctly visible over 50 feet below.  Traces of white mist cover the floor of the pit, becoming thicker to the right. " +
                "Marks in the dust around the window would seem to indicate that someone has been here recently. Directly across the pit from you and " +
                "25 feet away there is a similar window looking into a lighted room.  A  shadowy figure can be seen there peering back at you.";
            Level = 1;
            Items = new List<IAdventureItem> 
            {
                ItemFactory.GetInstance(Game, Item.ShadowyFigure),
            };

            ValidMoves = new List<IPlayerMove> 
            { 
                new PlayerMove(string.Empty, Location.Y2, "east", "e"),
            };
        }
    }
}

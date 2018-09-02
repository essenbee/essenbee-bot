using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class LowPassage : AdventureLocation
    {
        public LowPassage(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.LowPassage;
            Name = "Low Passage";
            ShortDescription = "in a low passage";
            LongDescription = "in a low N/S passage at a hole in the floor. The hole goes down to an E/ W passage.";
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.BarsOfSilver) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You crawl into a large open, echoing space...", Location.HallOfMountainKing, "south", "s"),
                new PlayerMove("", Location.Y2, "north", "n"),
                //new PlayerMove("", Location.BrokenPassage, "down", "d"),
            };
        }
    }
}

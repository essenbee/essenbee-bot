﻿using Essenbee.Bot.Core.Games.Adventure.Items;
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
            Moves = new Dictionary<string, Location>
                    {
                        {"east", Location.Building },
                        {"e", Location.Building },
                        {"enter", Location.Building },
                        {"in", Location.Building },
                        {"inside", Location.Building },
                        {"building", Location.Building },
                        {"south" , Location.Valley },
                        {"s" , Location.Valley },
                        {"west" , Location.Hill },
                        {"w" , Location.Hill },
                        {"road" , Location.Hill },
                        {"up" , Location.Hill },
                    };
        }
    }
}
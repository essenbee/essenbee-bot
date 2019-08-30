using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Oriental : AdventureLocation
    {
        public Oriental(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Oriental;
            Name = "Oriental room";
            ShortDescription = "in the Oriental room";
            LongDescription = "in the Oriental room. Ancient oriental cave drawings cover the " +
                "walls. A gently sloping passage leads upward to the North, another passage leads SE, " +
                "and a hands and knees crawl leads West.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(game, Item.Vase) };

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.SwissCheese, "southeast", "se"),
                new PlayerMove(string.Empty, Location.LowRoom, "west", "w", "crawl"),
                new PlayerMove(string.Empty, Location.MistyCavern, "up", "u", "north", "n", "cavern"),
            };
        }
    }
}

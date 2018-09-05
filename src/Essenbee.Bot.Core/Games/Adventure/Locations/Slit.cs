using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Slit : AdventureLocation
    {
        public Slit(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Slit;
            Name = "Slit";
            ShortDescription = "at the slit in the streambed";
            LongDescription = "besides the stream. At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.";
            WaterPresent = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Water) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.Valley, "valley", "north", "n", "upstream"),
                new PlayerMove(string.Empty, Location.Depression, "downstream", "south", "s"),
                new PlayerMove(string.Empty, Location.Forest, "forest", "east", "west", "e", "w"),
                new PlayerMove("You can't fit through a 2-inch slit!", Location.Slit, "down", "d"),
            };
        }
    }
}

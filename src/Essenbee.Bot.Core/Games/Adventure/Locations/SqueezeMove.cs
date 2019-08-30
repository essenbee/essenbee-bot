using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SqueezeMove : IPlayerMove
    {
        public List<string> Moves { get; }
        public Location Destination { get; }
        public string MoveText { get; set; }

        public SqueezeMove(string moveText, Location destination, params string[] moves)
        {
            Destination = destination;
            Moves = moves.ToList();
            MoveText = moveText;
        }

        public bool IsMatch(string move) => Moves.Any(v => move.Equals(v));

        public (bool, string) IsMoveAllowed(IAdventurePlayer player, IReadonlyAdventureGame game)
        {
            if (player.Inventory.Count() == 0)
            {
                return (true, string.Empty);
            }

            var message = "Its far too tight to squeeze through the gap whilst you are carrying things! " + 
                "Try dropping what you have with you, and maybe, just maybe, you could get through...";

            return (false, message);
        }
    }
}

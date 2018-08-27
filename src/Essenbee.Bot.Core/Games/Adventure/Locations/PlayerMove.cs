using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class PlayerMove
    {
        public List<string> Moves { get; }
        public Location Destination { get; }
        
        public PlayerMove(Location destination, params string[] moves)
        {
            Destination = destination;
            Moves = moves.ToList();
        }

        public bool IsMatch(string move) => Moves.Any(v => move.Equals(v));
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class PlayerMove : IPlayerMove
    {
        public List<string> Moves { get; }
        public Location Destination { get; }
        public string MoveText { get; set; }

        public PlayerMove(string moveText, Location destination, params string[] moves)
        {
            Destination = destination;
            Moves = moves.ToList();
            MoveText = moveText;
        }

        public bool IsMatch(string move) => Moves.Any(v => move.Equals(v));
    }
}

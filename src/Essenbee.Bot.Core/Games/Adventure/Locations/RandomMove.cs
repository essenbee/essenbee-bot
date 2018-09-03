using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class RandomMove : IPlayerMove
    {
        public Location Destination { get; }
        public List<string> Moves { get; }
        public string MoveText { get; set; }
        private IList<Location> _possibleDestinations;

        public RandomMove(string moveText, IList<Location> possibleDestinations, params string[] moves)
        { 
            Moves = moves.ToList();
            MoveText = moveText;
            _possibleDestinations = possibleDestinations;
        }

        public bool IsMatch(string move) => Moves.Any(v => move.Equals(v));
    }
}

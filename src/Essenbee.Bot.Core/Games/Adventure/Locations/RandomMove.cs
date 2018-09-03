using System;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class RandomMove : IPlayerMove
    {
        public Location Destination => GetRandomLocation();
        public List<string> Moves { get; }
        public string MoveText { get; set; }
        private IList<Location> _possibleDestinations;

        private static readonly Random getRandom = new Random();

        public RandomMove(string moveText, IList<Location> possibleDestinations, params string[] moves)
        { 
            Moves = moves.ToList();
            MoveText = moveText;
            _possibleDestinations = possibleDestinations;
        }

        public bool IsMatch(string move) => Moves.Any(v => move.Equals(v));

        private Location GetRandomLocation()
        {
            var loc = GetRandomNumber(0, _possibleDestinations.Count - 1);
            return _possibleDestinations[loc];
        }

        private int GetRandomNumber(int min, int max)
        {
            lock (getRandom) // synchronize
            {
                return getRandom.Next(min, max);
            }
        }
    }
}

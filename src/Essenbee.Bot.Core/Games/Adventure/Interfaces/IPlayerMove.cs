using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IPlayerMove
    {
        Location Destination { get; }
        List<string> Moves { get; }
        string MoveText { get; set; }

        bool IsMatch(string move);
    }
}
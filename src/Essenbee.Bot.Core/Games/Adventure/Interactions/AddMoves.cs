using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddMoves : IAction
    {
        private Dictionary<string, Location> _moves;
        private readonly AdventureLocation _toLocation;

        public AddMoves(Dictionary<string, Location> moves, AdventureLocation toLocation = null)
        {
            _moves = moves;
            _toLocation = toLocation;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            if (_toLocation is null)
            {
                player.CurrentLocation.AddMoves(_moves);
            }
            else
            {
                _toLocation.AddMoves(_moves);
            }
            
            return true;
        }
    }
}

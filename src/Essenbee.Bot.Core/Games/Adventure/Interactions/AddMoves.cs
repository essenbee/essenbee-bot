using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddMoves : IAction
    {
        private Dictionary<string, string> _moves;
        private readonly AdventureLocation _toLocation;

        public AddMoves(Dictionary<string, string> moves, AdventureLocation toLocation = null)
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

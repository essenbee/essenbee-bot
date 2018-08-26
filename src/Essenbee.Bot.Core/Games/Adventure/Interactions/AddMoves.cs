using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddMoves : IAction
    {
        private Dictionary<string, string> _moves;

        public AddMoves(Dictionary<string, string> moves)
        {
            _moves = moves;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            player.CurrentLocation.AddMoves(_moves);

            return true;
        }
    }
}

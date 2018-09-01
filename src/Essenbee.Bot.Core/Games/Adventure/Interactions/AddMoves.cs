using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddMoves : IAction
    {
        private List<IPlayerMove> _moves;
        private readonly Location _toLocation;
        private readonly IReadonlyAdventureGame _game;

        public AddMoves(List<IPlayerMove> moves, IReadonlyAdventureGame game, Location toLocation = Location.Nowhere)
        {
            _game = game;
            _moves = moves;
            _toLocation = toLocation;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            if (_toLocation == Location.Nowhere)
            {
                player.CurrentLocation.AddMoves(_moves);
            }
            else
            {
                var found = _game.TryGetLocation(_toLocation, out var loc);

                if (found)
                {
                    loc.AddMoves(_moves);
                }
            }
            
            return true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemovePlayerItemState : IAction
    {
        private string _state;

        public RemovePlayerItemState(string state) => _state = state;

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (item.PlayerItemState.ContainsKey(player.Id))
            {
                var states = item.PlayerItemState[player.Id];

                if (states.Contains(_state))
                {
                    states.Remove(_state);
                }
            }

            return true;
        }
    }
}

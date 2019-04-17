using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddPlayerItemState : IAction
    {
        private string _state;

        public AddPlayerItemState(string state) => _state = state;

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (item.PlayerItemState.ContainsKey(player.Id))
            {
                var states = item.PlayerItemState[player.Id];

                if (!states.Contains(_state))
                {
                    states.Add(_state);
                }
            }
            else
            {
                item.PlayerItemState.Add(player.Id, new List<string> { _state });
            }

            return true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddPlayerStatus : IAction
    {
        private PlayerStatus _status;

        public AddPlayerStatus(PlayerStatus status)
        {
            _status = status;
        }

        public bool Do(AdventurePlayer player, AdventureItem item = null)
        {
            if (player.Statuses.Contains(_status)) return false;

            player.Statuses.Add(_status);
            return true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemovePlayerStatus : IAction
    {
        private PlayerStatus _status;

        public RemovePlayerStatus(PlayerStatus status)
        {
            _status = status;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item = null)
        {
            if (!player.Statuses.Contains(_status)) return false;

            player.Statuses.Remove(_status);
            return true;
        }
    }
}

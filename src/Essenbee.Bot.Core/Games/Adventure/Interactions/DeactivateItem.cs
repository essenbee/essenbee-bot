using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class DeactivateItem : IAction
    {
        private string _message;

        public DeactivateItem(string message)
        {
            _message = message;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            item.IsActive = false;
            player.ChatClient.PostDirectMessage(player.Id, _message);

            return true;
        }
    }
}

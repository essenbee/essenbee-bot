using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Display : IAction
    {
        private string _message;

        public Display(string message)
        {
            _message = message;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item = null)
        {
            player.ChatClient.PostDirectMessage(player, _message);

            return true;
        }
    }
}

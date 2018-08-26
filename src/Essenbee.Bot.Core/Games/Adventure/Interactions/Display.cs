namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Display : IAction
    {
        private string _message;

        public Display(string message)
        {
            _message = message;
        }

        public bool Do(AdventurePlayer player, AdventureItem item = null)
        {
            player.ChatClient.PostDirectMessage(player.Id, _message);

            return true;
        }
    }
}

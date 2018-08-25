namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Open : IAction
    {
        private readonly IReadonlyAdventureGame _game;

        public Open(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item} here!");
                return false;
            }

            if (item.IsOpen)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is already open!");
                return false;
            }

            if (item.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is locked!");
                return false;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"You have opened the {item}.");
            item.IsOpen = true;
            return true;
        }
    }
}

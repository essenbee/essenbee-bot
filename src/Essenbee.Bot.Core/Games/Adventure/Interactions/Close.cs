using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Close : IAction
    {
        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player, $"You cannot see a {item.Name} here!");
                return false;
            }

            if (!item.IsOpen)
            {
                player.ChatClient.PostDirectMessage(player, $"The {item.Name} is already closed!");
                return false;
            }

            player.ChatClient.PostDirectMessage(player, $"You have closed the {item.Name}.");
            item.IsOpen = false;
            return true;
        }
    }
}

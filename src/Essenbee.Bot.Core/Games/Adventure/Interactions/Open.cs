using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Open : IAction
    {
        public bool Do(AdventurePlayer player, IAdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item.Name} here!");
                return false;
            }

            if (item.IsOpen)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item.Name} is already open!");
                return false;
            }

            if (item.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item.Name} is locked!");
                return false;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"You have opened the {item.Name}.");
            item.IsOpen = true;
            return true;
        }
    }
}

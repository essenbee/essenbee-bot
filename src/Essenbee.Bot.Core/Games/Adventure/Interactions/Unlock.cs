using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Unlock : IAction
    {
        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item} here!");
                return false;
            }

            if (!item.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is already unlocked!");
                return false;
            }

            if (!string.IsNullOrEmpty(item.ItemIdToUnlock))
            {
                if (player.Inventory.GetItems().All(i => i.ItemId != item.ItemIdToUnlock))
                {
                    player.ChatClient.PostDirectMessage(player.Id, $"You need a {item.ItemIdToUnlock} to unlock the {item}!");
                    return false;
                }

                player.ChatClient.PostDirectMessage(player.Id, $"You try your {item.ItemIdToUnlock} ...");
            }

            item.IsLocked = false;
            player.ChatClient.PostDirectMessage(player.Id, $"The {item} is now unlocked!");
            return true;
        }
    }
}

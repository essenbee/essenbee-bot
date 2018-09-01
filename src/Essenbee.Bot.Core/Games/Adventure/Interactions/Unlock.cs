using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Unlock : IAction
    {
        public bool Do(AdventurePlayer player, IAdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item.Name} here!");
                return false;
            }

            if (!item.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item.Name} is already unlocked!");
                return false;
            }

            if (player.Inventory.GetItems().All(i => i.ItemId != item.ItemIdToUnlock))
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You need to find something with which to unlock the {item.Name}!");
                return false;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"You try your {item.ItemIdToUnlock} ...");

            item.IsLocked = false;
            player.ChatClient.PostDirectMessage(player.Id, $"The {item.Name} is now unlocked!");
            return true;
        }
    }
}

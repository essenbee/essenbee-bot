using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Unlock : IAdventureCommand
    {
        private readonly IReadonlyAdventureGame _game;

        public Unlock(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var location = player.CurrentLocation;
            var item = e.ArgsAsList[1].ToLower();

            var locationItem = location.Items.FirstOrDefault(i => i.ItemId == item || i.ItemId == item);

            if (locationItem is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item} here!");
                return;
            }

            if (!locationItem.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is already unlocked!");
                return;
            }

            if (!string.IsNullOrEmpty(locationItem.ItemIdToUnlock))
            {
                if (player.Inventory.GetItems().All(i => i.ItemId != locationItem.ItemIdToUnlock))
                {
                    player.ChatClient.PostDirectMessage(player.Id, $"You need a {locationItem.ItemIdToUnlock} to unlock the {item}!");
                    return;
                }

                player.ChatClient.PostDirectMessage(player.Id, $"You try your {locationItem.ItemIdToUnlock} ...");
            }

            locationItem.IsLocked = false;
            player.ChatClient.PostDirectMessage(player.Id, $"The {item} is now unlocked!");
        }
    }
}

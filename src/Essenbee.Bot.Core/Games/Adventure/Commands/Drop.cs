using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Drop : BaseAdventureCommand
    {
        public Drop(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player.Id, "What would you like to drop? Try using: !adv drop _item_");
                return;
            }

            var itemToDrop = args[1];

            if (!player.Inventory.Has(itemToDrop))
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You don't have a {itemToDrop} to drop!");
            }
            else
            {
                var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToDrop));

                if (itemInInventory != null)
                {
                    player.Inventory.RemoveItem(itemInInventory);
                    player.CurrentLocation.Items.Add(itemInInventory);
                    player.ChatClient.PostDirectMessage(player.Id, $"You dropped a {itemToDrop}");
                }

                itemInInventory = player.Inventory.GetContainedItem(itemToDrop);

                if (itemInInventory != null)
                {
                    player.Inventory.RemoveItemContainer(itemInInventory);
                    player.CurrentLocation.Items.Add(itemInInventory);
                    player.ChatClient.PostDirectMessage(player.Id, $"You dropped a {itemToDrop}");
                    return;
                }

                // Just in case ...
                player.ChatClient.PostDirectMessage(player.Id, $"You don't have a {itemToDrop} to drop!");
            }
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Drop : BaseAdventureCommand
    {
        private const string All = "all";

        public Drop(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player, "What would you like to drop? Try using: !adv drop _item_");
                return;
            }

            var itemToDrop = args[1];

            if (itemToDrop.Equals(All))
            {
                foreach (var carriedItem in player.Inventory.GetItems())
                {
                    if (carriedItem.ItemId.Equals(Item.Lamp))
                    {
                        carriedItem.Interact("extinguish", player);
                    }
                    
                    player.Inventory.RemoveItem(carriedItem);
                    player.CurrentLocation.Items.Add(carriedItem);
                    player.ChatClient.PostDirectMessage(player, $"You dropped a {carriedItem.Name}");
                    carriedItem.RemovePlayerStatusCondition(player, carriedItem.GivesPlayerStatus);
                    
                }
            }
            else
            {
                if (!player.Inventory.Has(itemToDrop))
                {
                    player.ChatClient.PostDirectMessage(player, $"You don't have a {itemToDrop} to drop!");
                }
                else
                {
                    var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToDrop));

                    if (itemInInventory != null)
                    {
                        if (itemInInventory.ItemId.Equals(Item.Lamp))
                        {
                            itemInInventory.Interact("extinguish", player);
                        }
                        
                        player.Inventory.RemoveItem(itemInInventory);
                        player.CurrentLocation.Items.Add(itemInInventory);
                        player.ChatClient.PostDirectMessage(player, $"You dropped a {itemToDrop}");
                        itemInInventory.RemovePlayerStatusCondition(player, itemInInventory.GivesPlayerStatus);
                        

                        return;
                    }

                    itemInInventory = player.Inventory.GetContainedItem(itemToDrop);

                    if (itemInInventory != null)
                    {
                        player.Inventory.RemoveItemContainer(itemInInventory);
                        player.CurrentLocation.Items.Add(itemInInventory);
                        player.ChatClient.PostDirectMessage(player, $"You dropped a {itemToDrop}");
                        itemInInventory.RemovePlayerStatusCondition(player, itemInInventory.GivesPlayerStatus);
                        return;
                    }

                    // Just in case ...
                    player.ChatClient.PostDirectMessage(player, $"You don't have a {itemToDrop} to drop!");
                }
            }
        }
    }
}

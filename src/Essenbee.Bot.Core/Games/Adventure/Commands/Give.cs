using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Give : BaseAdventureCommand
    {
        public Give(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
            IsVerbatim = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player, "What would you like to give?");
                return;
            }

            IAdventureItem itemInInventory = null;
            IAdventureItem potentialRecipient = null;
            var giveTo = string.Empty;

            if (args.Count == 2)
            {
                var itemToGive = args[1].ToLower();

                if (!player.Inventory.Has(itemToGive))
                {
                    player.ChatClient.PostDirectMessage(player, $"You don't have a {itemToGive} to give to anyone!");
                    return;
                }

                itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToGive));
                potentialRecipient = player.CurrentLocation.Items.FirstOrDefault(x => x.IsCreature);
            }
            else
            {
                var potentialItem = args[1].ToLower();
                giveTo = args[2].ToLower();

                if (!player.Inventory.Has(potentialItem))
                {
                    giveTo = args[1].ToLower();
                    potentialItem = args[2].ToLower();

                    if (!player.Inventory.Has(potentialItem))
                    {
                        player.ChatClient.PostDirectMessage(player, $"You don't have a {potentialItem} to give to anyone!");
                        player.ChatClient.PostDirectMessage(player, "Try saying, !adv give _item_ to _recipient_...");

                        return;
                    }
                    else
                    {
                        itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(potentialItem));
                    }
                }
                else
                {
                    itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(potentialItem));
                }
            }

            if (player.Here(giveTo) ||
                (potentialRecipient != null && player.Here(potentialRecipient.ItemId)))
            {
                var recipient = !string.IsNullOrWhiteSpace(giveTo)
                    ? player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(giveTo))
                    : potentialRecipient;

                if (player.Here(recipient.ItemId))
                {
                    recipient.Give(player, itemInInventory, recipient);
                    return;
                }
            }

            player.ChatClient.PostDirectMessage(player, $"I'm not sure who you want to give the {itemInInventory.Name} to...");
        }
    }
}

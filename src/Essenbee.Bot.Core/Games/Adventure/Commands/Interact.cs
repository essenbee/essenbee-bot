using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Interact : BaseAdventureCommand
    {
        public Interact(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player, Verbs.Contains(args[0])
                        ? $"I don't know how to just `{args[0]}`. Can you be a little more explicit?"
                        : $"Sorry, I don't understand the verb `{args[0]}`.");

                return;
            }

            var itemToInteractWith = args[1];
            var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToInteractWith));

            if (itemInInventory is null)
            {
                var containedItems = player.Inventory.GetContainedItems();
                itemInInventory = containedItems.FirstOrDefault(i => i.IsMatch(itemToInteractWith));
            }

            var itemAtLocation = player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(itemToInteractWith));

            if (itemInInventory is null && itemAtLocation is null)
            {
                player.ChatClient.PostDirectMessage(player, $"Which `{itemToInteractWith}` are you referring to?");
                player.ChatClient.PostDirectMessage(player, "Tip: Don't use words like 'the' or 'at', try just <verb> <noun> instead.");
                return;
            }

            bool actionWasMatched;

            // Interact with inventory items as a preference ...
            actionWasMatched = itemInInventory?.Interact(args[0], player) ?? itemAtLocation.Interact(args[0], player);

            if (!actionWasMatched)
            {
                player.ChatClient.PostDirectMessage(player, $"I don't know how to `{args[0]}` a `{itemToInteractWith}`. " +
                    "Can you be clearer?");
            }
        }
    }
}

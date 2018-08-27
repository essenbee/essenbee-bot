using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Interact : BaseAdventureCommand
    {
        public Interact(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                if (Verbs.Contains(args[0]))
                {
                    player.ChatClient.PostDirectMessage(player.Id, $"I don't know how to just `{args[0]}`. Can you be a little more explicit?");
                }
                else
                {
                    player.ChatClient.PostDirectMessage(player.Id, $"Sorry, I don't understand `{args[0]}`.");
                }

                return;
            }

            var itemToInteractWith = args[1];
            var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToInteractWith));
            var itemAtLocation = player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(itemToInteractWith));

            if (itemInInventory is null && itemAtLocation is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"Which `{itemToInteractWith}` are you referring to?");
                return;
            }

            var actionWasMatched = false;

            // Interact with inventory items as a preference ...
            if (itemInInventory != null)
            {
                actionWasMatched = itemInInventory.Interact(args[0], player);
            }
            else
            {
                actionWasMatched = itemAtLocation.Interact(args[0], player);
            }

            if (!actionWasMatched)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"I don't know how to `{args[0]}` a `{itemToInteractWith}`. Can you be clearer?");
            }
        }
    }
}

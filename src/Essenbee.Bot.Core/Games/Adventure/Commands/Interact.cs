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

            var itemToUse = args[1];
            var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.Name == itemToUse || i.ItemId == itemToUse);

            if (itemInInventory == null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You don't have a `{itemToUse}` to use!");
                return;
            }

            var requestedAction = ResolveInteractionAlias(itemInInventory, args[0]);

            if (IsValidInteraction(itemInInventory, requestedAction))
            {
                itemInInventory.Interact(requestedAction, player);
                return;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"I don't know how to `{requestedAction}` a `{itemToUse}`. Can you be clearer?");
        }

        private string ResolveInteractionAlias(AdventureItem item, string originalRequest)
        {
            if (item.InteractionAlias != null && item.InteractionAlias.ContainsKey(originalRequest))
            {
                return item.InteractionAlias[originalRequest];
            }

            return originalRequest;
        }

        private bool IsValidInteraction(AdventureItem item, string requestedAction)
        {
            if (item is null || item.Interactions is null) return false;

            requestedAction += "-";
            return item.Interactions.Keys.Where(key => key.StartsWith(requestedAction)).Count() > 0;
        }
    }
}

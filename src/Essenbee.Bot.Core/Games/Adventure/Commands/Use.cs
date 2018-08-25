using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Use : IAdventureCommand
    {
        private readonly IReadonlyAdventureGame _game;

        public Use(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player.Id, "Which of your carried items would you like to use?");
                return;
            }

            var itemToUse = args[1];
            var itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.Name == itemToUse || i.ItemId == itemToUse);

            if (itemInInventory == null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You don't have a {itemToUse} to use!");
                return;
            }

            if (itemInInventory.Interactions != null && itemInInventory.Interactions.ContainsKey(args[0]))
            {
                itemInInventory.Interact(args[0], player);
                return;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"I don't know how to {args[0]} a {itemToUse}. Can you be clearer?");
        }
    }
}

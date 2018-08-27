using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveFromInventory : IAction
    {
        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            player.Inventory.RemoveItem(item);
            item = null;
            return true;
        }
    }
}

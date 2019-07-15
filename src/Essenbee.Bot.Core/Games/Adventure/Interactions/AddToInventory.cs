using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToInventory : IAction
    {
        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (!player.Inventory.Has(item.ItemId))
            {
                player.Inventory.AddItem(item);
                return true;
            }

            return false;
        }
    }
}

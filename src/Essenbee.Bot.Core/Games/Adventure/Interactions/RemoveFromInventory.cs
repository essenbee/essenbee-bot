using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveFromInventory : IAction
    {
        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (ContainerRequired(item))
            {
                var container = player.Inventory.GetItems().FirstOrDefault(i => i.ItemId == item.MustBeContainedIn);
                container?.Contents.Remove(item);
            }
            else
            {
                player.Inventory.RemoveItem(item);
            }

            return true;
        }

        private static bool ContainerRequired(IAdventureItem locationItem) => locationItem.MustBeContainedIn != Item.None;
    }
}

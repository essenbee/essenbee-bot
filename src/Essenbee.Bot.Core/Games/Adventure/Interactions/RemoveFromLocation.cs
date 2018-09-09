using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveFromLocation : IAction
    {
        private readonly IAdventureItem _itemToRemove;

        public RemoveFromLocation(IAdventureItem itemToRemove)
        {
            _itemToRemove = itemToRemove;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            player.CurrentLocation.RemoveItem(_itemToRemove);
            return true;
        }
    }
}

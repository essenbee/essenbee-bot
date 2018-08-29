using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveFromLocation : IAction
    {
        private readonly AdventureItem _itemToRemove;

        public RemoveFromLocation(AdventureItem itemToRemove)
        {
            _itemToRemove = itemToRemove;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            player.CurrentLocation.RemoveItem(_itemToRemove);
            return true;
        }
    }
}

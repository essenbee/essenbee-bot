using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveFromLocation : IAction
    {
        private readonly IAdventureItem _itemToRemove;
        private readonly IAdventureLocation _location;

        public RemoveFromLocation(IAdventureItem itemToRemove)
        {
            _itemToRemove = itemToRemove;
        }

        public RemoveFromLocation(IAdventureItem itemToRemove, IAdventureLocation location)
        {
            _itemToRemove = itemToRemove;
            _location = location;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (_location is null)
            {
                player.CurrentLocation.RemoveItem(_itemToRemove);
            }
            else
            {
                _location.RemoveItem(_itemToRemove);
            }

            return true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToLocation : IAction
    {
        private readonly AdventureItem _itemToAddToLocation;
        private readonly AdventureLocation _location;

        public AddToLocation(AdventureItem itemToAddToLocation)
        {
            _itemToAddToLocation = itemToAddToLocation;
        }

        public AddToLocation(AdventureItem itemToAddToLocation, AdventureLocation location)
        {
            _itemToAddToLocation = itemToAddToLocation;
            _location = location;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            if (_location is null)
            {
                player.CurrentLocation.AddItem(_itemToAddToLocation);
            }
            else
            {
                _location.AddItem(_itemToAddToLocation);
            }

            return true;
        }
    }
}

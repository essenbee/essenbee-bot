using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToLocation : IAction
    {
        private readonly IAdventureItem _itemToAddToLocation;
        private readonly IAdventureLocation _location;

        public AddToLocation(IAdventureItem itemToAddToLocation)
        {
            _itemToAddToLocation = itemToAddToLocation;
        }

        public AddToLocation(IAdventureItem itemToAddToLocation, IAdventureLocation location)
        {
            _itemToAddToLocation = itemToAddToLocation;
            _location = location;
        }

        public bool Do(AdventurePlayer player, IAdventureItem item)
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

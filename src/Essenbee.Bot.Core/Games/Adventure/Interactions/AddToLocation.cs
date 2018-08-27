using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToLocation : IAction
    {
        private readonly AdventureItem _itemToAddToLocation;

        public AddToLocation(AdventureItem itemToAddToLocation)
        {
            _itemToAddToLocation = itemToAddToLocation;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            player.CurrentLocation.AddItem(_itemToAddToLocation);
            return true;
        }
    }
}

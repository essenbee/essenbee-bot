using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class DisplayForLocation : IAction
    {
        private string _message;
        private readonly Location _atLocation;

        public DisplayForLocation(string message, Location atLocation)
        {
            _message = message;
            _atLocation = atLocation;
        }

        public bool Do(AdventurePlayer player, IAdventureItem item)
        {
            if (player.CurrentLocation.LocationId.Equals(_atLocation))
            {
                player.ChatClient.PostDirectMessage(player.Id, _message);
            }

            return true;
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IDungeon
    {
        IAdventureLocation GetStartingLocation();
        bool TryGetLocation(Location locationId, out IAdventureLocation place);
    }
}
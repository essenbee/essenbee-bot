using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IDungeon
    {
        Dictionary<Location, IAdventureLocation> Locations { get; }

        IAdventureLocation GetStartingLocation();
        bool TryGetLocation(Location locationId, out IAdventureLocation place);
    }
}
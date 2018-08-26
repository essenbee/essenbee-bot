using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.ObjectModel;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public interface IReadonlyAdventureGame
    {
        ReadOnlyCollection<AdventurePlayer> Players { get; }
        bool TryGetLocation(Location locationId, out AdventureLocation place);
    }
}

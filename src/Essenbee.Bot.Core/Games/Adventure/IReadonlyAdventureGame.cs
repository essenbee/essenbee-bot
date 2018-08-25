using System.Collections.ObjectModel;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public interface IReadonlyAdventureGame
    {
        ReadOnlyCollection<AdventurePlayer> Players { get; }
        bool TryGetLocation(string locationId, out AdventureLocation place);
    }
}

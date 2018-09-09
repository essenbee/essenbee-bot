using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IDungeonBuilder
    {
        Dictionary<Location, IAdventureLocation> Build(AdventureGame game);
    }
}
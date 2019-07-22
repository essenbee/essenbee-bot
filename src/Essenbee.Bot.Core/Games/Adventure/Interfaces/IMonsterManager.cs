using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IMonsterManager
    {
        List<WanderingMonster> Monsters { get; }
         Dictionary<Location, IAdventureLocation> Locations { get; }

        void Act(IReadonlyAdventureGame game, IAdventurePlayer player);
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IReadonlyAdventureGame
    {
        ReadOnlyCollection<AdventurePlayer> Players { get; }
        IDungeon Dungeon { get; }
        List<IMonsterManager> MonsterManagers { get; }

        void EndOfGame(IAdventurePlayer player);
    }
}

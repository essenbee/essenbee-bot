using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IReadonlyAdventureGame
    {
        ReadOnlyCollection<AdventurePlayer> Players { get; }
        IDungeon Dungeon { get; }

        List<WanderingMonster> WanderingMonsters { get; }

        void EndOfGame(IAdventurePlayer player);
    }
}

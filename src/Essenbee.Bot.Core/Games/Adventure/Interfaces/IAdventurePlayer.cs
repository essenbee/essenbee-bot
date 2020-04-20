using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventurePlayer
    {
        IChatClient ChatClient { get; set; }
        IAdventureLocation CurrentLocation { get; set; }
        string Id { get; set; }
        Inventory Inventory { get; set; }
        int Moves { get; set; }
        IAdventureLocation PriorLocation { get; set; }
        int Score { get; set; }
        IList<PlayerStatus> Statuses { get; set; }
        string UserName { get; set; }
        Dictionary<string, int> Clocks { get; set; }
        Dictionary<EventIds, int> EventRecord { get; set; }

        bool Here(Item item, IMonsterManager manager = null);
        bool Here(string item, IMonsterManager manager = null);
    }

}
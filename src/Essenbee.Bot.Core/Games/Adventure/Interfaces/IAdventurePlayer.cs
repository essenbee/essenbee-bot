using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using System;

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
    }

}
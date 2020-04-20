using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventurePlayer : IAdventurePlayer
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IAdventureLocation CurrentLocation { get; set; }
        public IAdventureLocation PriorLocation { get; set; }
        public IChatClient ChatClient { get; set; }
        public int Score { get; set; }
        public int Moves { get; set; }
        public Inventory Inventory { get; set; }
        public IList<PlayerStatus> Statuses { get; set; }
        public Dictionary<string, int> Clocks { get; set; } = new Dictionary<string, int>();
        public Dictionary<EventIds, int> EventRecord { get; set; } = new Dictionary<EventIds, int>();

        public AdventurePlayer()
        {
            Inventory = new Inventory();
            Statuses = new List<PlayerStatus>();
        }

        public AdventurePlayer(string userId, string userName, IChatClient chatClient)
        {
            Inventory = new Inventory();
            Statuses = new List<PlayerStatus>();
            Id = userId;
            UserName = userName;
            Score = 0;
            Moves = 0;
            ChatClient = chatClient;
        }

        public bool Here(Item item, IMonsterManager manager = null)
        {
            if (item == Item.Dwarf)
            {
                if (manager.Monsters.Any(d => (d.CurrentLocation != null) &&
                 d.CurrentLocation.LocationId.Equals(CurrentLocation.LocationId) &&
                 (d.Group == MonsterGroup.Dwarves)))
                {
                    return true;
                }
            }

            if (CurrentLocation.Items.Any(x => x.ItemId == item))
            {
                return true;
            }

            return CurrentLocation.Items.FirstOrDefault(i => i.Contents.Any(i => i.ItemId == item))
                != null;
        }

        public bool Here(string item, IMonsterManager manager = null)
        {
            if (item.ToLower().Equals("dwarf"))
            {
                if (manager.Monsters.Any(d => (d.CurrentLocation != null) &&
                 d.CurrentLocation.LocationId.Equals(CurrentLocation.LocationId) &&
                 (d.Group == MonsterGroup.Dwarves)))
                {
                    return true;
                }
            }

            if (CurrentLocation.Items.Any(i => i.IsMatch(item)))
            {
                return true;
            }
            
            return CurrentLocation.Items.FirstOrDefault(i => i.Contents.Any(i => i.IsMatch(item)))
                != null;
        }
    }
}

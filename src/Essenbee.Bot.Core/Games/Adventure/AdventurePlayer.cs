using Essenbee.Bot.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventurePlayer
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public AdventureLocation CurrentLocation { get; set; }
        public IChatClient ChatClient { get; set; }
        public int Score { get; set; }
        public int Moves { get; set; }
        public Inventory Inventory { get; set; }
        public IList<PlayerStatus> Statuses { get; set; }

        public AdventurePlayer()
        {
            Inventory = new Inventory();
            Statuses = new List<PlayerStatus>();
        }

        public bool HasRequiredContainer(AdventureItem item) => Inventory.GetItems().Any(i => i.ItemId == item.MustBeContainedIn);
    }
}

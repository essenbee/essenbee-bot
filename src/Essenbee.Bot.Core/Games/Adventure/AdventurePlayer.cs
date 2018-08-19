using Essenbee.Bot.Core.Interfaces;
using System.Collections.Generic;

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
        public IList<AdventureItem> Inventory { get; set; }

        public AdventurePlayer()
        {
            Inventory = new List<AdventureItem>();
        }
    }
}

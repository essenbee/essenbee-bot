using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureLocation
    {
        public string LocationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IList<AdventureItem> Items { get; set; }
        public IDictionary<string, string> Moves { get; set; }
        public bool WaterPresent { get; set; }

        public AdventureLocation()
        {
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string>();
        }

        public void AddMoves(Dictionary<string, string> newMoves)
        {
            newMoves.ToList().ForEach(x => Moves.Add(x.Key, x.Value));
        }

        public void RemoveDestination(string destination)
        {
            foreach (var move in Moves.Where(x => x.Value == destination).ToList())
            {
                Moves.Remove(move.Key);
            }
        }

        public void AddItem(AdventureItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(AdventureItem item)
        {
            Items.Remove(item);
        }

        public int Count() => Items.Count;
    }
}

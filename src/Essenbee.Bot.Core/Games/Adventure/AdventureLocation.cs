using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public abstract class AdventureLocation
    {
        public Location LocationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IReadonlyAdventureGame Game { get; }
        public IList<AdventureItem> Items { get; set; }
        public IDictionary<string, Location> Moves { get; set; }
        public bool WaterPresent { get; set; }
        public bool IsDark { get; set; }

        public AdventureLocation(IReadonlyAdventureGame game)
        {
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, Location>();
            Game = game;
        }

        public virtual void AddMoves(Dictionary<string, Location> newMoves)
        {
            newMoves.ToList().ForEach(x => Moves.Add(x.Key, x.Value));
        }

        public virtual void RemoveDestination(Location destination)
        {
            foreach (var move in Moves.Where(x => x.Value == destination).ToList())
            {
                Moves.Remove(move.Key);
            }
        }

        public virtual void AddItem(AdventureItem item)
        {
            Items.Add(item);
        }

        public virtual void RemoveItem(AdventureItem item)
        {
            Items.Remove(item);
        }

        public int Count() => Items.Count;
    }
}

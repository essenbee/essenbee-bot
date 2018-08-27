using Essenbee.Bot.Core.Games.Adventure.Interfaces;
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
        public IList<PlayerMove> ValidMoves { get; set; }
        public bool WaterPresent { get; set; }
        public bool IsDark { get; set; }

        public AdventureLocation(IReadonlyAdventureGame game)
        {
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove>();
            Game = game;
        }

        public virtual void AddMoves(List<PlayerMove> newMoves)
        {
            newMoves.ToList().ForEach(x => ValidMoves.Add(x));
        }

        public virtual void RemoveDestination(Location destination)
        {
            foreach (var move in ValidMoves.Where(x => x.Destination == destination).ToList())
            {
                ValidMoves.Remove(move);
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

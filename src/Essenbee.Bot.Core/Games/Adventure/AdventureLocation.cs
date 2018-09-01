using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public abstract class AdventureLocation : IAdventureLocation
    {
        public Location LocationId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IReadonlyAdventureGame Game { get; }
        public IList<IAdventureItem> Items { get; set; }
        public IList<IPlayerMove> ValidMoves { get; set; }
        public bool WaterPresent => Items.Any(i => i.ItemId.Equals(Item.Water));
        public bool IsDark { get; set; }

        protected AdventureLocation(IReadonlyAdventureGame game)
        {
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>();
            Game = game;
        }

        public virtual void AddMoves(List<IPlayerMove> newMoves)
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

        public virtual void AddItem(IAdventureItem item)
        {
            Items.Add(item);
        }

        public virtual void RemoveItem(IAdventureItem item)
        {
            Items.Remove(item);
        }

        public int Count() => Items.Count;
    }
}

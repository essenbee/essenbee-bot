using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class Inventory
    {
        private IList<AdventureItem> _items;

        public Inventory()
        {
            _items = new List<AdventureItem>();
        }

        public void AddItem(AdventureItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(AdventureItem item)
        {
            _items.Remove(item);
        }

        public int Count() => _items.Count;

        public IList<AdventureItem> GetItems() => _items;
    }
}

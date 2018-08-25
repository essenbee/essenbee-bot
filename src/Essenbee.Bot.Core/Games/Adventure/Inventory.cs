using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ReadOnlyCollection<AdventureItem> GetItems() => _items.ToList().AsReadOnly();

        public string ListItems()
        {
            if (_items.Count() == 0)
            {
                return "You are not carrying anything with you at the moment.";
            }

            var inventory = new StringBuilder("You are carrying these items with you:");
            inventory.AppendLine();

            foreach (var item in _items)
            {
                inventory.AppendLine($"\ta {item.Name}");
            }

            return inventory.ToString();
        }
    }
}

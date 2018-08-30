using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class Inventory
    {
        private IList<AdventureItem> _items;

        public Inventory()
        {
            _items = new List<AdventureItem>();
        }

        public bool AddItem(AdventureItem item)
        {
            _items.Add(item);
            return true;
        }

        public void RemoveItem(AdventureItem item)
        {
            _items.Remove(item);
        }

        public bool AddItemToContainer(AdventureItem item, Item containerId)
        {
            var intoContainer = _items.FirstOrDefault(i => i.IsContainer && i.ItemId.Equals(containerId));

            if (intoContainer is null)
            {
                return false;
            }

            intoContainer.Contents.Add(item);
            return true;
        }

        public bool RemoveItemContainer(AdventureItem item, Item containerId)
        {
            var fromContainer = _items.FirstOrDefault(i => i.IsContainer && i.ItemId.Equals(containerId));

            if (fromContainer is null)
            {
                return false;
            }

            fromContainer.Contents.Remove(item);
            return true;
        }

        public bool RemoveItemContainer(AdventureItem item)
        {
            var fromContainer = _items.FirstOrDefault(i => i.IsContainer && i.Contents.Contains(item));

            if (fromContainer is null)
            {
                return false;
            }

            fromContainer.Contents.Remove(item);
            return true;
        }

        public int Count() => _items.Count;

        public bool HasRequiredContainer(AdventureItem item) => _items.Any(i => i.ItemId == item.MustBeContainedIn);

        public ReadOnlyCollection<AdventureItem> GetItems() => _items.ToList().AsReadOnly();

        public ReadOnlyCollection<AdventureItem> GetContainedItems()
        {
            var containedItems = new List<AdventureItem>();

            foreach (var container in _items.Where(i => i.IsContainer))
            {
                containedItems.AddRange(container.Contents);
            }

            return containedItems.AsReadOnly();
        }

        public string ListItems()
        {
            if (!_items.Any())
            {
                return "You are not carrying anything with you at the moment.";
            }

            var inventory = new StringBuilder("You are carrying these items with you:");
            inventory.AppendLine();

            foreach (var item in _items)
            {
                var text = item.Contents.Any() 
                    ? $"\t* a {item.Name} containing:" 
                    : $"\t* a {item.Name}";
                inventory.AppendLine(text);

                if (item.Contents.Any())
                {
                    foreach (var content in item.Contents)
                    {
                        inventory.AppendLine($"\t\t- a {content.Name}");
                    }
                }
            }

            return inventory.ToString();
        }
    }
}

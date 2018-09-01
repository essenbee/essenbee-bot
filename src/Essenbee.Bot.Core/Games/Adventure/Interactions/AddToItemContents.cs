using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToItemContents : IAction
    {
        private readonly IAdventureItem _itemToAdd;

        public AddToItemContents(IAdventureItem itemToAdd)
        {
            _itemToAdd = itemToAdd;
        }

        public bool Do(AdventurePlayer player, IAdventureItem item)
        {
            item.Contents.Add(_itemToAdd);
            return true;
        }
    }
}

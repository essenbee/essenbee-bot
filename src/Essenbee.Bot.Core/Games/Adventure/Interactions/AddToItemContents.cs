namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddToItemContents : IAction
    {
        private readonly AdventureItem _itemToAdd;

        public AddToItemContents(AdventureItem itemToAdd)
        {
            _itemToAdd = itemToAdd;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            item.Contents.Add(_itemToAdd);
            return true;
        }
    }
}

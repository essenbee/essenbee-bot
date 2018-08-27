namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class UpdateItemName : IAction
    {
        private string _newName;

        public UpdateItemName(string newName)
        {
            _newName = newName;
        }

        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            item.Name = _newName;
            return true;
        }
    }
}

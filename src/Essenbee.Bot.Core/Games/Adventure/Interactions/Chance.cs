using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Chance : IAction
    {
        private int _pct;
        public Chance(int pct)
        {
            _pct = pct;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            return DieRoller.Percentage(_pct);
        }
    }
}

using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Chance : IAction
    {
        private static readonly Random getRandom = new Random();
        private int _pct;
        public Chance(int pct)
        {
            _pct = pct;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            var result = GetRandomNumber(0, 99);
            return result < _pct;
        }

        private int GetRandomNumber(int min, int max)
        {
            lock (getRandom) // synchronize
            {
                return getRandom.Next(min, max);
            }
        }
    }
}

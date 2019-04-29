using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class StartClock : IAction
    {
        private string _clockName;

        public StartClock(string clockName)
        {
            _clockName = clockName;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (!player.Clocks.ContainsKey(_clockName))
            {
                player.Clocks.Add(_clockName, 0);
            }

            return true;
        }
    }
}

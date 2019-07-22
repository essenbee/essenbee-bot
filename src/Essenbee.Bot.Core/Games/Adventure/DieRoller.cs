using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public static class DieRoller
    {
        private static readonly Random getRandom = new Random();

        public static bool Percentage(int target)
        {
           var roll = GetRandomNumber(0, 99);
           if (roll < target)
           {
               return true;
           }

            return false;
        }

        public static int Percentage()
        {
            return GetRandomNumber(0, 99);
        }

        public static bool SixSider(int target)
        {
            var roll = GetRandomNumber(0, 5);
            if (roll < target)
            {
                return true;
            }

            return false;
        }

        private static int GetRandomNumber(int min, int max)
        {
            lock (getRandom) // synchronize
            {
                return getRandom.Next(min, max);
            }
        }
    }
}

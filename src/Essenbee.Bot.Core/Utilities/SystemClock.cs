using Essenbee.Bot.Core.Interfaces;
using System;

namespace Essenbee.Bot.Core.Utilities
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}

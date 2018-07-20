using System;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface ITimer
    {
        TimeSpan Interval { get; }
    }
}

using System;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IDelayed
    {
        TimeSpan Delay { get; }
    }
}

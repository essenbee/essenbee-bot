using System;

namespace Essenbee.Bot.Core.Interfaces
{

    public interface IClock
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
    }
}

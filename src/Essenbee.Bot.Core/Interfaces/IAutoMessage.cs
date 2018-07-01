using System;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IAutoMessage
    {
        void Init(DateTime currentTime, ItemStatus status);
        bool ShouldDisplay(DateTime currentTime);
        string GetMessage(DateTime currentTime);
    }
}

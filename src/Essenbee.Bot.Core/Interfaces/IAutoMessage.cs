using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IAutoMessage
    {
        void Init(DateTime currentTime);
        bool ShouldDisplay(DateTime currentTime);
        string GetMessage(DateTime currentTime);
    }
}

using System;
using System.Linq.Expressions;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBackgroundAction
    {
        Expression<Action> ActionToPerform { get; }
    }
}

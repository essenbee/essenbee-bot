using System;
using System.Linq.Expressions;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        string CacheKey { get; }
    }
}

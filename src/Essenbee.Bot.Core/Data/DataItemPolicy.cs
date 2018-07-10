using Essenbee.Bot.Core.Interfaces;
using System;
using System.Linq.Expressions;

namespace Essenbee.Bot.Core.Data
{
    public class DataItemPolicy<T> : ISpecification<T> where T : DataEntity
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public string CacheKey => $"{typeof(T).Name}-{Criteria}";

        protected DataItemPolicy(Expression<Func<T, bool>> expression)
        {
            Criteria = expression;
        }

        public static DataItemPolicy<T> All()
        {
            return new DataItemPolicy<T>(x => true);
        }

        public static DataItemPolicy<T> ById(Guid id)
        {
            return new DataItemPolicy<T>(x => x.Id == id);
        }
    }
}

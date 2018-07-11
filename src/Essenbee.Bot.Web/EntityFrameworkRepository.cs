using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Web
{
    public class EntityFrameworkRepository : IRepository
    {
        private readonly AppDataContext _datastore;

        public EntityFrameworkRepository(AppDataContext datastore)
        {
            _datastore = datastore;
        }

        public T Create<T>(T dataItem) where T : DataEntity
        {
            _datastore.Set<T>().Add(dataItem);
            _datastore.SaveChanges();
            return dataItem;
        }

        public void Create<T>(List<T> dataItemList) where T : DataEntity
        {
            _datastore.Set<T>().AddRange(dataItemList);
            _datastore.SaveChanges();
        }

        public List<T> List<T>(ISpecification<T> spec = null) where T : DataEntity
        {
            DbSet<T> dbSet = _datastore.Set<T>();
            return spec != null ? dbSet.Where(spec.Criteria).ToList() : dbSet.ToList();
        }

        public void Remove<T>(T dataItem) where T : DataEntity
        {
            _datastore.Set<T>().Remove(dataItem);
            _datastore.SaveChanges();
        }

        public T Single<T>(ISpecification<T> spec) where T : DataEntity
        {
            return _datastore.Set<T>().SingleOrDefault(spec.Criteria);
        }

        public T Update<T>(T dataItem) where T : DataEntity
        {
            _datastore.Set<T>().Update(dataItem);
            _datastore.SaveChanges();
            return dataItem;
        }

        public void Update<T>(List<T> dataItemList) where T : DataEntity
        {
            _datastore.Set<T>().UpdateRange(dataItemList);
            _datastore.SaveChanges();
        }
    }
}

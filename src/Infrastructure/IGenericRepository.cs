using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(Guid id);
        IList<TEntity> GetAll();
        void AddOrUpdate(TEntity item);
        void AddOrUpdateAll(IList<TEntity> items);
        void Remove(TEntity item);

        void RemoveAll(IList<TEntity> items);
        bool Save();
    }
}
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetEntityById(Guid id);
        IEnumerable<T> GetEntitiesByQuery(Func<T, bool> query);
        T CreateOrUpdateEntity(T entity);
        void DeleteEntity(Guid id);
    }
}

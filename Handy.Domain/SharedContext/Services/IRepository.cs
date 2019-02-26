using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Handy.Domain.SharedContext.Services
{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        T GetByCriteria(Func<T, bool> criteria);
        IEnumerable<T> ListByCriteria(Func<T, bool> criteria);
        Task Persist(T item);
        Task Update(T item);
    }
}
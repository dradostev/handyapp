using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Handy.Domain.SharedContext.Services
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<T> GetByCriteria(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListByCriteria(Expression<Func<T, bool>> predicate, int limit = 10, int offset = 0);
        Task Persist(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
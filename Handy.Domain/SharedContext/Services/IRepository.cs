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
        Task<IEnumerable<T>> ListByCriteria(Expression<Func<T, bool>> predicate);
        Task Persist(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}
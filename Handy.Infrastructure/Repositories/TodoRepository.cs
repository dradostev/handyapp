using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handy.Domain.SharedContext.Services;
using Handy.Domain.TodoContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure.Repositories
{
    public class TodoRepository : IRepository<Todo>
    {
        private readonly HandyDbContext _db;

        public TodoRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public async Task<Todo> GetById(Guid id)
        {
            return await _db.Todos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Todo> GetByCriteria(Expression<Func<Todo, bool>> predicate)
        {
            return await _db.Todos.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Todo>> ListByCriteria(Expression<Func<Todo, bool>> predicate)
        {
            return await _db.Todos.Where(predicate).ToListAsync();
        }

        public async Task Persist(Todo item)
        {
            _db.Todos.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Todo item)
        {
            _db.Todos.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Todo item)
        {
            _db.Todos.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
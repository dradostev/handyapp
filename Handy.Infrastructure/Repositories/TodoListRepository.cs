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
    public class TodoListRepository : IRepository<TodoList>
    {
        private readonly HandyDbContext _db;

        public TodoListRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public async Task<TodoList> GetById(Guid id)
        {
            return await _db.TodoLists.Include(x => x.Todos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TodoList> GetByCriteria(Expression<Func<TodoList, bool>> predicate)
        {
            return await _db.TodoLists.Include(x => x.Todos).FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TodoList>> ListByCriteria(Expression<Func<TodoList, bool>> predicate)
        {
            return await _db.TodoLists.Where(predicate).Include(x => x.Todos).ToListAsync();
        }

        public async Task Persist(TodoList item)
        {
            _db.TodoLists.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(TodoList item)
        {
            _db.TodoLists.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TodoList item)
        {
            _db.TodoLists.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
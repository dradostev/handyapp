using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.SharedContext.Services;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure.Repositories
{
    public class ReminderRepository : IRepository<Reminder>
    {
        private readonly HandyDbContext _db;

        public ReminderRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public async Task<Reminder> GetById(Guid id)
        {
            return await _db.Reminders.Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Reminder> GetByCriteria(Expression<Func<Reminder, bool>> predicate)
        {
            return await _db.Reminders.Include(x => x.Account).FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Reminder>> ListByCriteria(Expression<Func<Reminder, bool>> predicate, int limit = 10, int offset = 0)
        {
            return await _db.Reminders
                .Include(x => x.Account)
                .Where(predicate)
                .OrderByDescending(x => x.Created)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task Persist(Reminder item)
        {
            _db.Reminders.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Reminder item)
        {
            _db.Reminders.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Reminder item)
        {
            _db.Reminders.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
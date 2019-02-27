using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.SharedContext.Services;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly HandyDbContext _db;

        public AccountRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public async Task<Account> GetById(Guid id)
        {
            return await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetByCriteria(Expression<Func<Account, bool>> predicate)
        {
            return await _db.Accounts.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Account>> ListByCriteria(Expression<Func<Account, bool>> predicate)
        {
            
            return await _db.Accounts.Where(predicate).ToListAsync();
        }

        public async Task Persist(Account item)
        {
            _db.Accounts.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Account item)
        {
            _db.Accounts.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var account = await GetById(id);
            _db.Accounts.Remove(account);
            await _db.SaveChangesAsync();
        }
    }
}
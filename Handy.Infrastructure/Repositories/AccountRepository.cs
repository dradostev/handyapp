using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.SharedContext.Services;

namespace Handy.Infrastructure.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly HandyDbContext _db;

        public AccountRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public Account GetById(Guid id)
        {
            return _db.Accounts.FirstOrDefault(x => x.Id == id);
        }

        public Account GetByCriteria(Func<Account, bool> criteria)
        {
            return _db.Accounts.FirstOrDefault(criteria);
        }

        public IEnumerable<Account> ListByCriteria(Func<Account, bool> criteria)
        {
            return _db.Accounts.Where(criteria);
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
    }
}
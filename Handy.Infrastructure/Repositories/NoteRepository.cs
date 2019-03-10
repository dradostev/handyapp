using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.SharedContext.Services;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure.Repositories
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly HandyDbContext _db;

        public NoteRepository(HandyDbContext db)
        {
            _db = db;
        }
        
        public async Task<Note> GetById(Guid id)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Note> GetByCriteria(Expression<Func<Note, bool>> predicate)
        {
            return await _db.Notes.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Note>> ListByCriteria(Expression<Func<Note, bool>> predicate, int limit = 10, int offset = 0)
        {
            return await _db.Notes.Where(predicate).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task Persist(Note item)
        {
            _db.Notes.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Note item)
        {
            _db.Notes.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Note item)
        {
            _db.Notes.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
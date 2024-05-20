using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity, CancellationToken token)
        {
            await dbSet.AddAsync(entity, token);
            await SaveAsync(token);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken token, Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync(token);
        }

        public async Task<T> GetAsync(CancellationToken token, Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync(filter, token);
        }

        public async Task RemoveAsync(T entity, CancellationToken token)
        {
            dbSet.Remove(entity);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
           => await _db.SaveChangesAsync(token);
    }
}

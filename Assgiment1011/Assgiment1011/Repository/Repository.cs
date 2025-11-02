
using Assgiment1011.Data;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using System;
using System.Linq.Expressions;

namespace Assgiment1011.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;

        internal DbSet<T> dbSet { get; set; }
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null
                                          , string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if(filter != null)
            {
                query = query.Where(filter);

            }

            if (pageSize > 0)
            {
                if(pageSize > 100)
                {
                    pageSize = 100;
                }

                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) { 
                    query = query.Include(includeProp);

                }
            }


            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp); 
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryWithPage(IQueryable<T> query, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            if (query == null)
                query = dbSet;

            if(pageSize > 0) { 
                if(pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                { query = query.Include(includeProp);}
            }

            return query;
        }
    }
}

using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assgiment1011.Repository
{
    public class AnnoucementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        private readonly ApplicationDBContext _db;
        public AnnoucementRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync(DateTime? createdDate, string? titleSearch= null, string? slugFilter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<Announcement> query = _db.Announcements.AsQueryable();

            if(createdDate.HasValue)
            {
                query = query.Where(it => it.CreatedDate >= createdDate);
            }

            if (!String.IsNullOrWhiteSpace(titleSearch))
            {
                query = query.Where(c => c.Title.ToLower().Contains(titleSearch.ToLower()));
            }

            if(!String.IsNullOrWhiteSpace(slugFilter))
            {
                query = query.Where(c => c.Slug.ToLower().Contains(slugFilter.ToLower()));
            }

            query = query.OrderByDescending(it => it.CreatedDate);
            query = base.GetQueryWithPage(query, includeProperties, pageSize, pageNumber);

            return await query.ToListAsync();

        }

        public async Task<Announcement> UpdateAsync(Announcement entity)
        {
            _db.Update<Announcement>(entity);

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

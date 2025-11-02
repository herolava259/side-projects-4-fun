using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assgiment1011.Repository
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly ApplicationDBContext _db;
        public AnswerRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Answer>> GetAllAsync(DateTime? startDate, DateTime? endDate, string? contentFilter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<Answer> query = _db.Answers.AsQueryable();


            if (startDate.HasValue)
            {
                query = query.Where(c => c.CreatedDate >=  startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(c => c.CreatedDate <= endDate);
            }

            if (!String.IsNullOrWhiteSpace(contentFilter))
            {
                query = query.Where(c => c.Content.ToLower().Contains(contentFilter.ToLower()));
            }

            query = query.OrderByDescending(c => c.CreatedDate);
            query = base.GetQueryWithPage(query, includeProperties, pageSize, pageNumber);

            return await query.ToListAsync();

        }

        public async Task<Answer> UpadteAsync(Answer entity)
        {
            _db.Update<Answer>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

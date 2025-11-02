using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDBContext _db;
        public AuthorRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            _db.Update<Author>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

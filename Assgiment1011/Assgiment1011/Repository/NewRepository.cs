using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class NewRepository : Repository<New>, INewRepository
    {
        private readonly ApplicationDBContext db;

        public NewRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<New> UpdateAsync(New entity)
        {
            db.Update<New>(entity);
            await db.SaveChangesAsync();
            return entity;  
        }
    }
}

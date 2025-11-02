using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        private readonly ApplicationDBContext db;

        public VideoRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<Video> UpdateAsync(Video entity)
        {
            db.Update<Video>(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

using Assgiment1011.Data;
using Assgiment1011.Models.Media;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository.MediaRepository
{
    public class VideoDetailRepository : Repository<VideoDetail>, IVideoDetailRepository
    {
        private readonly ApplicationDBContext db;
        public VideoDetailRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<VideoDetail> UpadteAsync(VideoDetail entity)
        {
            db.Update(entity);
            await db.SaveChangesAsync
                ();
            return entity;
        }
    }
}

using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class VideoGalleryRepository : Repository<VideoGallery>, IVideoGalleryRepository
    {
        private readonly ApplicationDBContext db;

        public VideoGalleryRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<VideoGallery> UpdateAsync(VideoGallery entity)
        {
            db.Update<VideoGallery>(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

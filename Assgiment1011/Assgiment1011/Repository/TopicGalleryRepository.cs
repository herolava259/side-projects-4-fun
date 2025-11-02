using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class TopicGalleryRepository : Repository<TopicGallery>, ITopicGalleryRepository
    {
        private readonly ApplicationDBContext db;

        public TopicGalleryRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<TopicGallery> UpdateAsync(TopicGallery entity)
        {
            db.Update<TopicGallery>(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

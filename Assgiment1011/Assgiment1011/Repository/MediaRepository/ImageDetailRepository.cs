using Assgiment1011.Data;
using Assgiment1011.Models.Media;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository.MediaRepository
{
    public class ImageDetailRepository : Repository<ImageDetail>, IImageDetailRepository
    {
        private readonly ApplicationDBContext db;
        public ImageDetailRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<ImageDetail> UpadteAsync(ImageDetail entity)
        {
            db.Update(entity);
            await db.SaveChangesAsync
                ();
            return entity;
        }
    }
}

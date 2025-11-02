using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class ImageGalleryRepository : Repository<ImageGallery>, IImageGalleryRepository
    {
        private readonly ApplicationDBContext _db;

        public ImageGalleryRepository(ApplicationDBContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<ImageGallery> UpdateAsync(ImageGallery entity)
        {
            _db.Update<ImageGallery>(entity); 
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

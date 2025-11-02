using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly ApplicationDBContext _db;

        public ImageRepository(ApplicationDBContext db) : base(db)
        {
            this._db = db;
        }

        async Task<Image> IImageRepository.UpdateAsync(Image entity)
        {
            _db.Update<Image>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class DocumentGalleryRepository : Repository<DocumentGallery>, IDocumentGalleryRepository
    {
        private readonly ApplicationDBContext _db;
        public DocumentGalleryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<DocumentGallery> UpdateAsync(DocumentGallery entity)
        {
            _db.Update<DocumentGallery>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IDocumentGalleryRepository : IRepository<DocumentGallery>
    {
        Task<DocumentGallery> UpdateAsync(DocumentGallery entity);
    }
}

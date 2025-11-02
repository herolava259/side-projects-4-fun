using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IImageGalleryRepository : IRepository<ImageGallery>
    {
        Task<ImageGallery> UpdateAsync(ImageGallery entity);
    }
}

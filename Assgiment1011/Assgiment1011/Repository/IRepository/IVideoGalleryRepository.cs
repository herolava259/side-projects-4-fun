using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IVideoGalleryRepository:IRepository<VideoGallery>
    {
        Task<VideoGallery> UpdateAsync(VideoGallery entity);
    }
}

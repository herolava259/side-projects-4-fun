using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface ITopicGalleryRepository : IRepository<TopicGallery>
    {
        Task<TopicGallery> UpdateAsync(TopicGallery entity);
    }
}

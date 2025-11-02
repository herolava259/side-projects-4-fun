using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IVideoRepository: IRepository<Video>
    {
        Task<Video> UpdateAsync(Video entity);
    }
}

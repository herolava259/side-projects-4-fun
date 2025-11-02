using Assgiment1011.Models;
using Assgiment1011.Models.Media;

namespace Assgiment1011.Repository.IRepository
{
    public interface IVideoDetailRepository: IRepository<VideoDetail>
    {
        Task<VideoDetail> UpadteAsync(VideoDetail entity);
    }
}

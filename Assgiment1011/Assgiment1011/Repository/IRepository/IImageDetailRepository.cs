using Assgiment1011.Models;
using Assgiment1011.Models.Media;

namespace Assgiment1011.Repository.IRepository
{
    public interface IImageDetailRepository: IRepository<ImageDetail>
    {
        Task<ImageDetail> UpadteAsync(ImageDetail entity);
    }
}

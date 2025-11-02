using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IImageRepository:IRepository<Image>
    {
        Task<Image> UpdateAsync(Image entity);
    }
}

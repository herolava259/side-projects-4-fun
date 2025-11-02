using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface INewRepository: IRepository<New>
    {
        Task<New> UpdateAsync(New entity);
    }
}

using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<Author> UpdateAsync(Author entity);
    }
}

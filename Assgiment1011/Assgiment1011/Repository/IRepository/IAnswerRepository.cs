using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IAnswerRepository: IRepository<Answer>
    {
        Task<Answer> UpadteAsync(Answer entity);

        Task<IEnumerable<Answer>> GetAllAsync(DateTime? startDate, DateTime? endDate,
                                 string? contentFilter = null,
                                 string? includeProperties = null,
                                 int pageSize = 0,
                                 int pageNumber = 1);

    }
}

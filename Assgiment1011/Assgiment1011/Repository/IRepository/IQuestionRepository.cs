using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IQuestionRepository: IRepository<Question>
    {
        Task<Question> UpdateAsync(Question entity);
    }
}

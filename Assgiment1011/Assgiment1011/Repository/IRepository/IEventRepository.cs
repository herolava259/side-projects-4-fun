using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IEventRepository: IRepository<Event>
    {
        Task<Event> UpdateAsync(Event entity);
    }
}

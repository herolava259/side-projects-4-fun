using Assgiment1011.Models;
using System.Linq.Expressions;

namespace Assgiment1011.Repository.IRepository
{
    public interface IAnnouncementRepository: IRepository<Announcement>
    {
        Task<Announcement> UpdateAsync(Announcement entity);
        Task<IEnumerable<Announcement>> GetAllAsync(DateTime? createdDate, string? titleSearch, string? slugFilter,
                                           string? includeProperties = null, int pageSize = 0, int pageNumber = 1);
    }
}

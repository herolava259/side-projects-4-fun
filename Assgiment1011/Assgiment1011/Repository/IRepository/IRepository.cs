using System.Linq.Expressions;

namespace Assgiment1011.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
                                         int pageSize = 0, int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null,bool tracked= true, string? includeProperties = null);

        IQueryable<T> GetQueryWithPage(IQueryable<T> query, string? includeProperties = null, int pageSize =0 , int pageNumber = 1);

        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}

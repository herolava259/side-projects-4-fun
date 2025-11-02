using Assgiment1011.Models.RestfulModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Assgiment1011.OptimalRepository.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetEntities();

        Task<TEntity> GetEntity(Guid id);

        Task<IEnumerable<TEntity>> UpdatesEntities(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateEntity(TEntity entity, Tuple<String, SqlParameter[]> condition);
        Task<bool> UpdateEntity(TEntity entity);

        Task<TEntity> AddEntity(TEntity entity);
        
        Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities);


        Task<bool> DeleteEntity(TEntity entity);

        Task<bool> DeleteEntity(Guid id);

        Task<bool> DeleteEntities(IEnumerable<TEntity> entities);


        Task<BaseDataCollection<TEntity>> QueryEntities(PageModel page);
    }
}

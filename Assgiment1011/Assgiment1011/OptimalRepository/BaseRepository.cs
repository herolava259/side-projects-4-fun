using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Models.RestfulModel;
using Assgiment1011.OptimalRepository.IRepository;
using Assgiment1011.Utilities.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Assgiment1011.OptimalRepository
{
    public abstract partial class BaseRepository<TEntity>: IDisposable, IBaseRepository<TEntity> where TEntity : EntityBase
    {
        protected ApplicationDBContext _db;

        protected readonly Func<ApplicationDBContext> _dbFactory;

        public BaseRepository(Func<ApplicationDBContext> func)
        {
            _dbFactory = func;
            _db = _dbFactory.Invoke();
        }
        public Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddEntity(TEntity entity)
        {
            if(ValidationHelper.GetValidation)
            {
                RulesEngineValid(entity);
            }

            var now = DateTime.UtcNow;
            entity.CreatedDate = now;
            entity.UpdatedDate = now;

            entity.SyncStatus = entity.IsSyncJob() && !entity.GetIsDuplicated() ? SyncStatus.Synchroized: SyncStatus.NotSync;
            _db.Add(entity);

            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteEntities(IEnumerable<TEntity> entities)
        {
            _db.RemoveRange(entities);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEntity(TEntity entity)
        {
            _db.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<TEntity>> GetEntities()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetEntity(Guid id)
        {
            var entity = await _db.FindAsync<TEntity>(id);

            return entity;
        }

        public Task<BaseDataCollection<TEntity>> QueryEntities(PageModel page)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateEntity(TEntity entity, Tuple<string, SqlParameter[]> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            if(ValidationHelper.GetValidation)
            {
                RulesEngineValid(entity);
            }

            if (entity.IsSyncJob())
            {
                switch(entity.IgnoreIsSync)
                {
                    case SetSyncOption.SetSync:
                        entity.SyncStatus = SyncStatus.Synchroized; break;
                    case SetSyncOption.SetNotSync:
                        entity.SyncStatus= SyncStatus.NotSync; break;
                    case SetSyncOption.Keep:
                        break;

                }

            }
            else
            {
                entity.SyncStatus = SyncStatus.NotSync;
            }
            return true;
        }

        public Task<IEnumerable<TEntity>> UpdatesEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEntity(Guid id)
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);
            if(entity == null)
            {
                return false;
            }

            _db.Remove(entity);

            return await _db.SaveChangesAsync() > 0;

        }
    }
}

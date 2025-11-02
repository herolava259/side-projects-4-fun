using Assgiment1011.OptimalRepository.IRepository;
using Assgiment1011.Utilities.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assgiment1011.OptimalRepository
{
    public abstract partial class BaseRepository<TEntity> 
    {
        protected virtual void Dispose(bool disposing) {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        private bool RulesEngineValid(TEntity entity)
        {
            return true;
        }

        private void ComparePropertyModifies(TEntity entity)
        {
            Type readonlyAttributeType = typeof(ReadonlyAttribute);

            _db.Entry<TEntity>(entity).Properties.ToList().ForEach(p =>
            {
                var currentValue = p.CurrentValue?.ToString();
                var originalValue = p.OriginalValue?.ToString();

                if (currentValue != originalValue ||
                  (p.Metadata.Name == "CreatedBy" || p.Metadata.Name == "CreatedDate") ||
                  (Attribute.IsDefined(p.Metadata.PropertyInfo, readonlyAttributeType)) ){
                    p.IsModified = false;
                }
                else
                {
                    p.IsModified = true;
                }
            });
        }
    }
}

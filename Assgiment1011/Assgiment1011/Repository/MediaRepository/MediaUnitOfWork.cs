using Assgiment1011.Data;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Assgiment1011.Repository.MediaRepository
{
    public class MediaUnitOfWork : IMediaUnitOfWork, IDisposable
    {
        private readonly ApplicationDBContext _db;
        private IVideoDetailRepository? _vidRepo;
        private IImageDetailRepository? _imgRepo;
        private IDocumentDetailRepository? _docRepo;

        private IDbContextTransaction? _objTran;
        public MediaUnitOfWork(ApplicationDBContext db)
        {
            _db = db;
        }

        public ApplicationDBContext Context
        {
            get
            {
                return _db;
            }
        }

        public IImageDetailRepository ImageDetailRepository
        {
            get
            {
                if (_imgRepo == null)
                    _imgRepo = new ImageDetailRepository(_db);
                return _imgRepo;
            }
        }

        public IVideoDetailRepository VideoDetailRepository
        {
            get
            {
                if(_vidRepo == null)
                {
                    _vidRepo = new VideoDetailRepository(_db);
                }

                return _vidRepo;
            }
        }

        public IDocumentDetailRepository DocumentDetailRepository
        {
            get
            {
                if(_docRepo == null)
                {
                    _docRepo = new DocumentDetailRepository(_db);
                }

                return _docRepo;
            }
        }

        public void Commit()
        {
            _objTran?.Commit();
        }

        public IDbContextTransaction CreateTransaction()
        {
            _objTran = _db.Database.BeginTransaction();
            return _objTran;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

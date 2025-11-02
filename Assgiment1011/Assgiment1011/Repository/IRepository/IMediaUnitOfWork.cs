using Assgiment1011.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Assgiment1011.Repository.IRepository
{
    public interface IMediaUnitOfWork
    {
        ApplicationDBContext Context { get; }

        IImageDetailRepository ImageDetailRepository { get; }

        IVideoDetailRepository VideoDetailRepository { get; }

        IDocumentDetailRepository DocumentDetailRepository { get; }

        IDbContextTransaction CreateTransaction();

        void Commit();

        void Rollback();

        Task SaveAsync();
    }
}

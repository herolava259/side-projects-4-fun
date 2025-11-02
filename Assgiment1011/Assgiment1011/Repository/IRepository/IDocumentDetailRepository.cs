using Assgiment1011.Models.Media;

namespace Assgiment1011.Repository.IRepository
{
    public interface IDocumentDetailRepository:IRepository<DocumentDetail>, IMediaRepository<DocumentDetail>
    {
        Task<DocumentDetail> UpdateAsync(DocumentDetail documentDetail);

    }
}

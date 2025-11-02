using Assgiment1011.Models;

namespace Assgiment1011.Repository.IRepository
{
    public interface IDocumentRepository: IRepository<Document>
    {
        Task<Document> UpdateAsync(Document entity);

        Task<Document?> GetDocumentsByAuthorNameCompiledAsync(string authorName);
    }
}

using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EFUtil = Microsoft.EntityFrameworkCore.EF;
namespace Assgiment1011.Repository
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly ApplicationDBContext _db;

        private static readonly Func<ApplicationDBContext, string, Task<Document?>> GetByAuthorNameWithCompiledQuery =
            EFUtil.CompileAsyncQuery(
                (ApplicationDBContext context, string authorName) =>
                            context.Documents.AsNoTracking()
                                   .Include(x => x.Author)
                                   .Where(c => c.Author.Name.ToLower().Contains(authorName.ToLower()) )
                                   .FirstOrDefault()
                );
        public DocumentRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Document> UpdateAsync(Document entity)
        {
            _db.Update<Document>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Document?> GetDocumentsByAuthorNameCompiledAsync(string authorName)
        {
            
            return (await GetByAuthorNameWithCompiledQuery(_db,authorName));
        }
    }
}

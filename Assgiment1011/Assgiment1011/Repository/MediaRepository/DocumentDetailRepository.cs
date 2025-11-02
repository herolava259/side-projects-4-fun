using Assgiment1011.Data;
using Assgiment1011.Models.Media;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository.MediaRepository
{
    public class DocumentDetailRepository : Repository<DocumentDetail>, IDocumentDetailRepository
    {
        private readonly ApplicationDBContext db;

        public DocumentDetailRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<DocumentDetail> UpdateAsync(DocumentDetail documentDetail)
        {
            db.Update(documentDetail);
            await db.SaveChangesAsync
                ();
            return documentDetail;
        }
    }
}

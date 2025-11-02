using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly ApplicationDBContext db;

        public QuestionRepository(ApplicationDBContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<Question> UpdateAsync(Question entity)
        {
            db.Update<Question>(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

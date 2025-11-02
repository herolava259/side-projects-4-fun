using Assgiment1011.Data;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;

namespace Assgiment1011.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDBContext _db;

        public EventRepository(ApplicationDBContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            _db.Update<Event>(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Data.Repository
{
    public class DumUserRepository : Repository<DumUser>, IDumUserRepository
    {
        private ApplicationDbContext _db;
        public DumUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DumUser obj)
        {
            _db.DumUsers.Update(obj);
        }

    }
}

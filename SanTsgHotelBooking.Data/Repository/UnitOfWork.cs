using SanTsgHotelBooking.Data.Repository.IRepository;

namespace SanTsgHotelBooking.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            DumUser = new DumUserRepository(_db);
            Cities = new CityRepository(_db);
        }

        public IDumUserRepository DumUser { get; private set; }
        public ICityRepository Cities { get; private set; }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

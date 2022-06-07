using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Data.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(City obj)
        {
            _db.Cities.Update(obj);
        }
    }
}

using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Data.Repository
{
    public class DumUserRepository : Repository<DumUser>, IDumUserRepository
    {
        private ApplicationDbContext _db;
        public DumUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}

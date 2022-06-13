using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Booking obj)
        {
            _db.Bookings.Update(obj);
        }

    }
}

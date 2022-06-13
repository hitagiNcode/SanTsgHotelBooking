using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking obj);
    }
}

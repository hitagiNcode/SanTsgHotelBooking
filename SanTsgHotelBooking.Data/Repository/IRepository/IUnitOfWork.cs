namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDumUserRepository DumUser { get; }

        ICityRepository Cities { get; }

        IBookingRepository Bookings { get; }
        void Save();
    }
}

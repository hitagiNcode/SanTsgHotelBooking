namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDumUserRepository DumUser { get; }

        void Save();
    }
}

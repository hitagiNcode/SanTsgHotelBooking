using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface IDumUserRepository : IRepository<DumUser>
    {
        void Update(DumUser obj);
    }
}

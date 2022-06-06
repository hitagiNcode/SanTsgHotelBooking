using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface IDumUserRepository : IRepository<DumUser>
    {
        void Update(DumUser obj);
    }
}

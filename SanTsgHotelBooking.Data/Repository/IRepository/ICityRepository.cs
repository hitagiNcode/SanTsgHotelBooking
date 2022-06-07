using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Data.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void update(City obj);
    }
}

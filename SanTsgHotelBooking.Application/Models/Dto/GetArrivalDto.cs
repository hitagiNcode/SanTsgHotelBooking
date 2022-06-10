using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models.Dto
{
    public class GetArrivalDto
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
    }
}

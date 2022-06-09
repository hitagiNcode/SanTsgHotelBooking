using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models
{
    public class HotelSearchRequest
    {
        public string ProductType { get; set; }
        public string Query { get; set; }
        public string Culture { get; set; }

    }
}

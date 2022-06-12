using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models.GetProductInfoRequest
{
    public class GetProductInfoRequest
    {
        public int productType { get; set; } = 2;
        public int ownerProvider { get; set; } = 2;
        public string product { get; set; } = null!;
        public string culture { get; set; } = "en-US";
    }
}

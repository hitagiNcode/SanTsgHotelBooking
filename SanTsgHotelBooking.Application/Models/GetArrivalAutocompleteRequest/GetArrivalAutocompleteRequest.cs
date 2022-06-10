using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models.GetArrivalAutocompleteRequest
{
    public class GetArrivalAutocompleteRequest
    {
        public int ProductType { get; set; } = 2;
        public string Query { get; set; }
        public string Culture { get; set; } = "en-US";
    }
}

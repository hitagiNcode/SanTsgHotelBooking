using System.ComponentModel.DataAnnotations;


namespace SanTsgHotelBooking.Domain
{
    public class HotelProduct
    {
        public string City { get; set; } = "";
        public string HotelId { get; set; } = "";
        public string HotelName { get; set; } = "";
    }
}

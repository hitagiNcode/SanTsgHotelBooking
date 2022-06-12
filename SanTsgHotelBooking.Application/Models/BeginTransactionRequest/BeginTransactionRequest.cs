namespace SanTsgHotelBooking.Application.Models.BeginTransactionRequest
{
    public class BeginTransactionRequest
    {
        public List<string> offerIds { get; set; } = null!;
        public string currency { get; set; } = "EUR";
        public string culture { get; set; } = "en-US";
    }
}

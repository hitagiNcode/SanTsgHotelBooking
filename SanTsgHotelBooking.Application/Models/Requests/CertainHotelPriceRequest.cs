namespace SanTsgHotelBooking.Application.Models.Requests
{
    public class CertainHotelPriceRequest
    {
        public bool checkAllotment { get; set; } = true;
        public bool checkStopSale { get; set; } = true;
        public bool getOnlyDiscountedPrice { get; set; } = false;
        public bool getOnlyBestOffers { get; set; } = true;
        public int productType { get; set; } = 2;
        public List<string> Products { get; set; } = new List<string> { "101044" };
        public List<RoomCriterion> roomCriteria { get; set; } = new List<RoomCriterion> { new RoomCriterion { adult = 2 } };
        public string nationality { get; set; } = "DE";
        public string checkIn { get; set; } = "2023-06-20";
        public int night { get; set; } = 1;
        public string currency { get; set; } = "EUR";
        public string culture { get; set; } = "en-US";
    }

    public class RoomCriterion
    {
        public int adult { get; set; }
    }
}

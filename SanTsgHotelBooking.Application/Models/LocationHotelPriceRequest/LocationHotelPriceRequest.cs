namespace SanTsgHotelBooking.Application.Models.LocationHotelPriceRequest
{
    public class LocationHotelPriceRequest
    {
        public bool checkAllotment { get; set; } = true;
        public bool checkStopSale { get; set; } = true;
        public bool getOnlyDiscountedPrice { get; set; } = false;
        public bool getOnlyBestOffers { get; set; } = true;
        public int productType { get; set; } = 2;
        public List<ArrivalLocation> arrivalLocations { get; set; } = new List<ArrivalLocation> { new ArrivalLocation { id = "23494", type = 2 } };
        public List<RoomCriterion> roomCriteria { get; set; } = new List<RoomCriterion> { new RoomCriterion { adult = 2 } };
        public string nationality { get; set; } = "DE";
        public string checkIn { get; set; } = "2023-06-20";
        public int night { get; set; } = 7;
        public string currency { get; set; } = "EUR";
        public string culture { get; set; } = "en-US";
    }
    public class ArrivalLocation
    {
        public string id { get; set; }
        public int type { get; set; } = 2;
    }

    public class RoomCriterion
    {
        public int adult { get; set; }
        public List<int> childAges { get; set; }
    }
}

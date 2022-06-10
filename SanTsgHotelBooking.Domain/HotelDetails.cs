namespace SanTsgHotelBooking.Domain
{
    public class HotelDetails
    {

        public string HotelName { get; set; }
        public string HotelId { get; set; }
        public string ThumbnailFullUrl { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string HotelCityId { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePage { get; set; }


        public class Hotel
        {
            public List<Season> seasons { get; set; }
        }

        public class MediaFile
        {
            public int fileType { get; set; }
            public string url { get; set; }
            public string urlFull { get; set; }
        }

        public class Season
        {
            public string name { get; set; }
            public List<MediaFile> mediaFiles { get; set; }
        }
    }
}

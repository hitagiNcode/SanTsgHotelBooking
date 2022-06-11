using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models.LocationHotelPriceResponse
{
    public class LocationHotelPriceResponse
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Board
    {
        public string id { get; set; }
    }

    public class Body
    {
        public string searchId { get; set; }
        public DateTime expiresOn { get; set; }
        public List<Hotel> hotels { get; set; }
        public Details details { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class Description
    {
        public string text { get; set; }
    }

    public class Details
    {
        public bool enablePaging { get; set; }
        public bool getOnlyBestOffers { get; set; }
    }

    public class Geolocation
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class GiataInfo
    {
        public string hotelId { get; set; }
        public string destinationId { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Hotel
    {
        public Geolocation geolocation { get; set; }
        public string stars { get; set; }
        public List<Theme> themes { get; set; }
        public List<Board> boards { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        public GiataInfo giataInfo { get; set; }
        public Town town { get; set; }
        public Village village { get; set; }
        public List<Offer> offers { get; set; }
        public string address { get; set; }
        public int provider { get; set; }
        public string thumbnail { get; set; }
        public string thumbnailFull { get; set; }
        public Description description { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
    }

    public class Offer
    {
        public int night { get; set; }
        public bool isAvailable { get; set; }
        public List<Room> rooms { get; set; }
        public bool isRefundable { get; set; }
        public string offerId { get; set; }
        public DateTime checkIn { get; set; }
        public Price price { get; set; }
        public bool ownOffer { get; set; }
    }

    public class Price
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Room
    {
        public string roomId { get; set; }
        public string roomName { get; set; }
        public List<object> roomGroups { get; set; }
        public string accomId { get; set; }
        public string accomName { get; set; }
        public string boardId { get; set; }
        public string boardName { get; set; }
        public List<object> boardGroups { get; set; }
        public int allotment { get; set; }
        public int stopSaleGuaranteed { get; set; }
        public int stopSaleStandart { get; set; }
        public Price price { get; set; }
        public List<Traveller> travellers { get; set; }
    }

    public class Theme
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Town
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class Traveller
    {
        public int type { get; set; }
    }

    public class Village
    {
        public string name { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }


}

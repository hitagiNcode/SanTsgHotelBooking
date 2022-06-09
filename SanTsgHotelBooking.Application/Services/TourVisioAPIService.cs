using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SanTsgHotelBooking.Application.Models;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Domain;
using SanTsgHotelBooking.Shared.SettingsModels;
using System.Net.Http.Headers;
using System.Text;

namespace SanTsgHotelBooking.Application.Services
{
    public class TourVisioAPIService : ITourVisioAPIService
    {
        private readonly TourvisioAPISettings _tourvisioAPISettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public string? tourVisioJWT { get; private set; }

        public TourVisioAPIService(IOptions<TourvisioAPISettings> tourvisioAPISettings, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _tourvisioAPISettings = tourvisioAPISettings.Value;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelProduct>> SearchHotels(string city, string token)
        {
            List<HotelProduct> hotels = new List<HotelProduct>();

            string searchUrl = _tourvisioAPISettings.WebService + "/api/productservice/getarrivalautocomplete";
            HttpClient client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HotelSearchRequest searchRequest = new HotelSearchRequest { ProductType = "2", Query = city, Culture = "en-US" };
            var serializeSearch = System.Text.Json.JsonSerializer.Serialize(searchRequest);
            StringContent stringContent = new StringContent(serializeSearch, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(searchUrl, stringContent);
            var apiContent = await response.Content.ReadAsStringAsync();
            Models.HotelProductRequest.Root myHotels = JsonConvert.DeserializeObject<Models.HotelProductRequest.Root>(apiContent);
            for (int i = 0; i < myHotels.body.items.Count; i++)
            {
                if (myHotels.body.items[i].hotel != null)
                {
                    HotelProduct newHotel = new HotelProduct();
                    newHotel.City = myHotels.body.items[i].city.name;
                    newHotel.HotelId = myHotels.body.items[i].hotel.id;
                    newHotel.HotelName = myHotels.body.items[i].hotel.name;
                    hotels.Add(newHotel);
                }
            }

            return hotels;
        }

        public async Task<HotelDetails> GetHotelDetails(int id, string token)
        {
            HotelDetails hotelDetails;
            string searchUrl = _tourvisioAPISettings.WebService + "/api/productservice/getproductInfo";
            HttpClient client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HotelInfoRequestVars hotelInfoReq = new HotelInfoRequestVars { product = id.ToString()};
            var serializeRequest = System.Text.Json.JsonSerializer.Serialize(hotelInfoReq);
            StringContent stringContent = new StringContent(serializeRequest, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(searchUrl, stringContent);
            var apiContent = await response.Content.ReadAsStringAsync();
            Models.HotelInfoRequest.Root hotelInfoDetails = JsonConvert.DeserializeObject<Models.HotelInfoRequest.Root>(apiContent);
            hotelDetails = new HotelDetails { HotelName = hotelInfoDetails.body.hotel.name, HotelId = hotelInfoDetails.body.hotel.id, CityName = hotelInfoDetails.body.hotel.city.name, 
                CountryName = hotelInfoDetails.body.hotel.country.name, HomePage = hotelInfoDetails.body.hotel.homePage, HotelCityId = hotelInfoDetails.body.hotel.city.id,
            PhoneNumber = hotelInfoDetails.body.hotel.phoneNumber, ThumbnailFullUrl = hotelInfoDetails.body.hotel.thumbnailFull};

            return hotelDetails;
        }

        public async Task<string> LoginTourVisio()
        {
            string loginUrl = _tourvisioAPISettings.WebService + "/api/authenticationservice/login";
            HttpClient client = _httpClientFactory.CreateClient();
            var serializeProduct = System.Text.Json.JsonSerializer.Serialize(_tourvisioAPISettings);
            StringContent stringContent = new StringContent(serializeProduct, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.GetAsync(apiUrl+loginPath).Result;
            var response = await client.PostAsync(loginUrl, stringContent);
            var apiContent = await response.Content.ReadAsStringAsync();
            Models.TokenRequest.Root myToken = JsonConvert.DeserializeObject<Models.TokenRequest.Root>(apiContent);
            return tourVisioJWT = myToken.body.token;
        }
    }
}

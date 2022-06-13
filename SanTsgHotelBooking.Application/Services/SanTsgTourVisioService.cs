using Microsoft.Extensions.Options;
using SanTsgHotelBooking.Application.Models.BeginTransactionRequest;
using SanTsgHotelBooking.Application.Models.CertainHotelPriceRequest;
using SanTsgHotelBooking.Application.Models.GetArrivalAutocompleteRequest;
using SanTsgHotelBooking.Application.Models.GetProductInfoRequest;
using SanTsgHotelBooking.Application.Models.LocationHotelPriceRequest;
using SanTsgHotelBooking.Application.Models.Requests;
using SanTsgHotelBooking.Application.Models.TourVisioLoginRequest;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Shared.SettingsModels;

namespace SanTsgHotelBooking.Application.Services
{
    public class SanTsgTourVisioService : BaseService, ISanTsgTourVisioService
    {
        private readonly TourvisioAPISettings _tourvisioAPISettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public SanTsgTourVisioService(IHttpClientFactory httpClientFactory, IOptions<TourvisioAPISettings> tourvisioAPISettings) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _tourvisioAPISettings = tourvisioAPISettings.Value;
        }

        public async Task<T> AuthLoginTourVisioAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new TourVisioLoginRequest() { Agency = _tourvisioAPISettings.Agency, User = _tourvisioAPISettings.User, Password = _tourvisioAPISettings.Password },
                Url = _tourvisioAPISettings.WebService + "/api/authenticationservice/login",
                AccessToken = ""
            });
        }

        public async Task<T> GetArrivalAutoCompleteAsync<T>(string city, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new GetArrivalAutocompleteRequest() { Query = city},
                Url = _tourvisioAPISettings.WebService + "/api/productservice/getarrivalautocomplete",
                AccessToken = token
            });
        }

        public async Task<T> LocationHotelPriceSearchAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new LocationHotelPriceRequest() { arrivalLocations = new List<ArrivalLocation> { new ArrivalLocation { id = id.ToString(), type = 2 } } },
                Url = _tourvisioAPISettings.WebService + "/api/productservice/pricesearch",
                AccessToken = token
            });
        }

        public async Task<T> GetHotelDetailsByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new GetProductInfoRequest() { product = id.ToString() },
                Url = _tourvisioAPISettings.WebService + "/api/productservice/getproductInfo",
                AccessToken = token
            });
        }

        public async Task<T> GetHotelPriceAsync<T>(int id, int personAmount, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new CertainHotelPriceRequest() { Products = new List<string> { id.ToString() }, roomCriteria = new List<Models.CertainHotelPriceRequest.RoomCriterion> { new Models.CertainHotelPriceRequest.RoomCriterion { adult = personAmount } } },
                Url = _tourvisioAPISettings.WebService + "/api/productservice/pricesearch",
                AccessToken = token
            });
        }

        public async Task<T> BeginTransactionAsync<T>(string offerId, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Shared.StaticDetails.ApiType.POST,
                Data = new BeginTransactionRequest() { offerIds = new List<string> { offerId } },
                Url = _tourvisioAPISettings.WebService + "/api/bookingservice/begintransaction",
                AccessToken = token
            });
        }

        public Task<T> CommitTransactionAsync<T>(string transactionId, string token)
        {
            throw new NotImplementedException();
        }



        public Task<T> GetReservationDetailAsync<T>(string resevationNumber, string token)
        {
            throw new NotImplementedException();
        }

        public Task<T> SetReservationInfoAsync<T>(string transactionId, string token)
        {
            throw new NotImplementedException();
        }

    }
}

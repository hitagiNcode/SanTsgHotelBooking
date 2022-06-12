﻿using Microsoft.Extensions.Options;
using SanTsgHotelBooking.Application.Models.GetArrivalAutocompleteRequest;
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

        public Task<T> BeginTransactionAsync<T>(string offerId)
        {
            throw new NotImplementedException();
        }

        public Task<T> CommitTransactionAsync<T>(string transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetHotelDetailsByIdAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetReservationDetailAsync<T>(string resevationNumber)
        {
            throw new NotImplementedException();
        }

        public Task<T> SetReservationInfoAsync<T>(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
namespace SanTsgHotelBooking.Application.Services.IServices
{
    public interface ISanTsgTourVisioService : IBaseService
    {
        Task<T> AuthLoginTourVisioAsync<T>();
        Task<T> GetArrivalAutoCompleteAsync<T>(string city, string token);
        Task<T> LocationHotelPriceSearchAsync<T>(int id, string token);
        Task<T> GetHotelDetailsByIdAsync<T>(int id, string token);
        Task<T> BeginTransactionAsync<T>(string offerId);
        Task<T> SetReservationInfoAsync<T>(string transactionId);
        Task<T> CommitTransactionAsync<T>(string transactionId);
        Task<T> GetReservationDetailAsync<T>(string resevationNumber);
    }
}

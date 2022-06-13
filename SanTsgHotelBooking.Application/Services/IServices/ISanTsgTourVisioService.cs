namespace SanTsgHotelBooking.Application.Services.IServices
{
    public interface ISanTsgTourVisioService : IBaseService
    {
        Task<T> AuthLoginTourVisioAsync<T>();
        Task<T> GetArrivalAutoCompleteAsync<T>(string city, string token);
        Task<T> LocationHotelPriceSearchAsync<T>(int id, string token);
        Task<T> GetHotelDetailsByIdAsync<T>(int id, string token);
        Task<T> GetHotelPriceAsync<T>(int id,int personAmount, string token);
        Task<T> BeginTransactionAsync<T>(string offerId, string token);
        Task<T> SetReservationInfoAsync<T>(string transactionId, string firstname, string lastname, string email, string token);
        Task<T> CommitTransactionAsync<T>(string transactionId, string token);
        Task<T> GetReservationDetailAsync<T>(string resevationNumber, string token);
    }
}

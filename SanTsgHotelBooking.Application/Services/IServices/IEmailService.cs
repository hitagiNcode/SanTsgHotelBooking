using SanTsgHotelBooking.Application.Models.Requests;

namespace SanTsgHotelBooking.Application.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

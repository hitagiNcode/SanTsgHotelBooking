using SanTsgHotelBooking.Application.Models;

namespace SanTsgHotelBooking.Application.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

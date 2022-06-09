using SanTsgHotelBooking.Application.Models.HotelInfoRequest;
using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Services.IServices
{
    public interface ITourVisioAPIService
    {
        Task<string> LoginTourVisio();
        Task<IEnumerable<HotelProduct>> SearchHotels(string city, string token);
        Task<HotelDetails> GetHotelDetails(int id, string token);
    }
}

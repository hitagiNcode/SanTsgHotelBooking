using AutoMapper;
using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Application.Models.Profiles
{
    public class HotelDetailsProfile : Profile
    {
        public HotelDetailsProfile()
        {
            CreateMap<HotelInfoRequest.Hotel, HotelDetails>();
        }
    }
}

using Authentication_API.Dtos;
using Authentication_API.Models;
using Authentication_API.Services.DataServices;
using AutoMapper;

namespace Authentication_API.AppMapping
{
    public class AppMappingService: Profile
    {
        public AppMappingService()
        {
            CreateMap<Guest, GuestDto>().ReverseMap();
            CreateMap<Guest, RegisterDto>().ReverseMap();
        }
    }
}

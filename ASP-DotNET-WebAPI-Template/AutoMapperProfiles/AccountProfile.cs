using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Models;
using AutoMapper;

namespace ASP_DotNET_WebAPI_Template.AutoMapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AppUser, UserDTO>().ReverseMap();
        }
    }
}
using ASP_DotNET_WebAPI_Template.DTOs;
using ASP_DotNET_WebAPI_Template.Models;
using AutoMapper;

namespace ASP_DotNET_WebAPI_Template.AutoMapperProfiles
{
    public class ForecastProfile : Profile
    {
        public ForecastProfile()
        {
            CreateMap<WeatherForecast, GetForecastDTO>();
            CreateMap<CreateForecastDto, WeatherForecast>();
        }
    }
}
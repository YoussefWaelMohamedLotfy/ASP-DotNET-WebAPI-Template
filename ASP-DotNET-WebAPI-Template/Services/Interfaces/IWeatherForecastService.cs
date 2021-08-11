using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DTOs;

namespace ASP_DotNET_WebAPI_Template.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<GetForecastDTO> GetForecastById(int id);
        Task AddForecast(CreateForecastDto dto);
        Task DeleteForecast(int id);
    }
}
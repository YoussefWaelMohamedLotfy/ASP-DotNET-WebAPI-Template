using System;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.Models;

namespace ASP_DotNET_WebAPI_Template.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<WeatherForecast> WeatherForecasts { get; }

        Task<int> SaveChangesAsync();
    }
}
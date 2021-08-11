using ASP_DotNET_WebAPI_Template.Services;
using Microsoft.Extensions.DependencyInjection;
using ASP_DotNET_WebAPI_Template.Repositories;
using ASP_DotNET_WebAPI_Template.Repositories.Interfaces;
using ASP_DotNET_WebAPI_Template.Services.Interfaces;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class WebAppServiceExtension
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            // Add all your services here
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
        }
    }
}
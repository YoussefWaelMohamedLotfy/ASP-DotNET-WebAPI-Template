using ASP_DotNET_WebAPI_Template.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class WebAppServiceExtension
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            // Add all your services here
            services.AddTransient<LogService>();
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration Configuration)
        {
            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];

            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                options.AddPolicy("AllowOrigins", builder => builder.WithOrigins(allowedOrigins).AllowCredentials());
                options.AddPolicy("PublicAPI", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("Content-Type"));
            });
        }
    }
}
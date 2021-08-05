using Marvin.Cache.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class ResponseCachingExtension
    {
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();

            services.AddHttpCacheHeaders(
                (expirationOption) =>
                {
                    expirationOption.MaxAge = 120;
                    expirationOption.CacheLocation = CacheLocation.Private;
                },
                (validationOption) =>
                {
                    validationOption.MustRevalidate = true;
                }
            );
        }
    }
}
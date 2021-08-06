using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class ApiVersioningExtension
    {
        const string CUSTOM_HEADER_NAME = "custom-version-header";

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Comment the next line if you don't want to show the API version in response headers
                config.ReportApiVersions = true;

                // Minimum Setup for Versioning to set the default API version number if none exists
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;

                // Extra Configuration for HTTP Header-Based Versioning
                config.ApiVersionReader = new HeaderApiVersionReader(CUSTOM_HEADER_NAME);

                // Extra Configuration for HTTP Media Type-Based Versioning
                config.ApiVersionReader = new MediaTypeApiVersionReader();
            });
        }
    }
}
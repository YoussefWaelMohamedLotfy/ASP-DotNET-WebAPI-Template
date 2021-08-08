using System;
using ASP_DotNET_WebAPI_Template.HealthChecks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class HealthCheckExtension
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                // .AddSqlServer(
                //     configuration.GetConnectionString("DefaultConnection"),
                //     name: "SQL Server Check",
                //     failureStatus: HealthStatus.Unhealthy,
                //     tags: new[] { "ready" })
                .AddUrlGroup(
                    new Uri("https://www.google.com"),
                    "API Check",
                    HealthStatus.Degraded,
                    timeout: new TimeSpan(0, 0, 5),
                    tags: new[] { "ready" })
                .AddFileWriteCheck("File Check", HealthStatus.Unhealthy, new[] { "ready" });
        }
    }
}
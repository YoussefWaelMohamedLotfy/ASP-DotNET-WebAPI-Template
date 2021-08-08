using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class HealthCheckMiddlewareExtension
    {
        public static void MapCustomHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    ResultStatusCodes = {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    },
                    ResponseWriter = WriteReadyHealthCheckResponse,
                    Predicate = (check) => check.Tags.Contains("ready"),
                    AllowCachingResponses = false
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    Predicate = (check) => !check.Tags.Contains("ready"),
                    ResponseWriter = WriteLiveHealthCheckResponse,
                    AllowCachingResponses = false
                });

                endpoints.MapHealthChecksUI();

                endpoints.MapHealthChecks("/healthui", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
        }

         private static Task WriteReadyHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.000")),
                new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
                    new JProperty(dicItem.Key, new JObject(
                        new JProperty("Status", dicItem.Value.Status.ToString()),
                        new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0.000")),
                        new JProperty("Exception", dicItem.Value.Exception?.Message),
                        new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData =>
                            new JProperty(dicData.Key, dicData.Value))))
                    ))
                )))
            );

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }

        private static Task WriteLiveHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.000"))
            );

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
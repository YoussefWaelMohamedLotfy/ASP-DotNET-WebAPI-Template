using System.Collections.Generic;
using ASP_DotNET_WebAPI_Template.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileWriteHealthCheckBuilderExtension
    {
        public static IHealthChecksBuilder AddFileWriteCheck(this IHealthChecksBuilder builder, string name, HealthStatus failureStatus, IEnumerable<string> tags = default)
        {
            return builder.Add(new HealthCheckRegistration(name, new FileWriteHealthCheck(), failureStatus, tags));
        }
    }
}
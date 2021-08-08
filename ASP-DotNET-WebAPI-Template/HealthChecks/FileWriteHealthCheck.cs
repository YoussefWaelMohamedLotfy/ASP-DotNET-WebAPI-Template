using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ASP_DotNET_WebAPI_Template.HealthChecks
{
    public class FileWriteHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var testFilePath = "HealthTest.txt";
                var fileStream = File.Open(testFilePath, FileMode.Append, FileAccess.Write);
                fileStream.Write(Encoding.UTF8.GetBytes("Testing..."));
                fileStream.Flush();
                fileStream.Close();
                File.Delete(testFilePath);

                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                switch (context.Registration.FailureStatus)
                {
                    case HealthStatus.Degraded:
                        return Task.FromResult(HealthCheckResult.Degraded("Issue writing to file", ex));

                    default:
                        return Task.FromResult(HealthCheckResult.Unhealthy("Issue writing to file", ex));
                }
            }
        }
    }
}
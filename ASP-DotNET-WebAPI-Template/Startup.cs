using System;
using System.Linq;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DbContexts;
using ASP_DotNET_WebAPI_Template.Extensions;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASP_DotNET_WebAPI_Template
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuring SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuring JWT Authentication
            services.AddAuthentication();
            services.ConfigureIdentity();

            // Configuring AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Configuring Caching
            services.ConfigureHttpCacheHeaders();

            // Configuring Rate Limiting and Throttling
            services.AddMemoryCache();
            services.ConfigureRateLimiting();

            // Configuring App Services
            services.ConfigureAppServices();

            // Configuring API Versioning
            services.ConfigureApiVersioning();

            // Configuring Health Checks
            services.ConfigureHealthChecks(Configuration);
            
            services.ConfigureSwagger();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP_DotNET_WebAPI_Template v1"));
            }

            app.UseHttpsRedirection();

            // Caching Middleware
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            // Rate Limit Middleware
            app.UseIpRateLimiting();

            app.UseRouting();

            // Authentication Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Built-in Exception Handling Middleware
            app.ConfigureBuiltInExceptionHandler(loggerFactory);

            // Custom Exception Handling Middleware
            //app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Endpoint for Health Check
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

                endpoints.MapHealthChecks("health/live", new HealthCheckOptions()
                {
                    Predicate = (check) => !check.Tags.Contains("ready"),
                    ResponseWriter = WriteLiveHealthCheckResponse,
                    AllowCachingResponses = false
                });
            });
        }

        private Task WriteReadyHealthCheckResponse(HttpContext httpContext, HealthReport result)
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

        
        private Task WriteLiveHealthCheckResponse(HttpContext httpContext, HealthReport result)
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

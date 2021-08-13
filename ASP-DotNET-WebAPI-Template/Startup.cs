using ASP_DotNET_WebAPI_Template.DbContexts;
using ASP_DotNET_WebAPI_Template.Extensions;
using AspNetCoreRateLimit;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            // Configuring SQLite
            //services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

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

            // Configuring Hangfire Server
            services.ConfigureHangfire(Configuration);

            // Configuring Sendgrid
            services.ConfigureSendGrid(Configuration);

            services.ConfigureSwagger();

            // Configuring CORS
            services.ConfigureCors(Configuration);

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
            app.UseRouting();

            app.UseCors("AllowEverything");

            // Caching Middleware
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            // Rate Limit Middleware
            app.UseIpRateLimiting();

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

                // Health Check Endpoints Middleware
                endpoints.MapCustomHealthChecks();

                endpoints.MapHangfireDashboard();
            });
        }
    }
}

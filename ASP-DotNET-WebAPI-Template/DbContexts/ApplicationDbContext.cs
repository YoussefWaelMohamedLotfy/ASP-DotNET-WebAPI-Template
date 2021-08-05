using ASP_DotNET_WebAPI_Template.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_DotNET_WebAPI_Template.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
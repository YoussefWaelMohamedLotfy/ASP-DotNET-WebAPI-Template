using ASP_DotNET_WebAPI_Template.Configurations;
using ASP_DotNET_WebAPI_Template.Services;
using ASP_DotNET_WebAPI_Template.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_DotNET_WebAPI_Template.Extensions
{
    public static class SendGridExtension
    {
        public static void ConfigureSendGrid(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<ISendGridEmailService, SendGridEmailService>();
            
            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];
            });
        }
    }
}
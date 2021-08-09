using System.Threading.Tasks;
using SendGrid;

namespace ASP_DotNET_WebAPI_Template.Services.Interfaces
{
    public interface ISendGridEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
    }
}
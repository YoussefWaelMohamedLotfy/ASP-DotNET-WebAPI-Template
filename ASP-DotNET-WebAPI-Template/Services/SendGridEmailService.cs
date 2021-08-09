using ASP_DotNET_WebAPI_Template.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using ASP_DotNET_WebAPI_Template.Configurations;
using System.Threading.Tasks;

namespace ASP_DotNET_WebAPI_Template.Services
{
    public class SendGridEmailService : ISendGridEmailService
    {
        private SendGridEmailSenderOptions _options { get; set; }

        public SendGridEmailService(IOptions<SendGridEmailSenderOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            await Execute(_options.ApiKey, subject, message, recipientEmail);
        }

        private async Task<Response> Execute(string apiKey, string subject, string message, string receipientEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_options.SenderEmail, _options.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(receipientEmail));

            return await client.SendEmailAsync(msg);
        }
    }
}
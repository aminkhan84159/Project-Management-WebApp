using Microsoft.Extensions.Configuration;
using Nexus.API.DataService.IDataService;
using System.Net.Mail;
using System.Net;

namespace Nexus.API.DataService.DataService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string recipient, string subject, string body)
        {
            var email = _configuration.GetValue<string>("Email_Configuration:Email");
            var password = _configuration.GetValue<string>("Email_Configuration:Password");
            var host = _configuration.GetValue<string>("Email_Configuration:Host");
            var port = _configuration.GetValue<int>("Email_Configuration:Port");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email, password);

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true // Important for sending HTML content
            };
            mailMessage.To.Add(new MailAddress(recipient));

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

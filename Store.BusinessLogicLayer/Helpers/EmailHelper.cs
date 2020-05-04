using Microsoft.Extensions.Configuration;
using Store.BusinessLogicLayer.Helpers.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Email.Constants;

namespace Store.BusinessLogicLayer.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfigurationSection _configurationSection;
        public EmailHelper(IConfiguration configuration)
        {
            _configurationSection = configuration.GetSection(EmailConstants.EmailConfig);
        }

        public async Task<bool> SendMessageAsync(string userEmail, string body, string subject)
        {
            var mailMessage = new MailMessage();           
            mailMessage.From = new MailAddress(_configurationSection.GetSection(EmailConstants.EmailForSending).Value);
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = _configurationSection.GetSection(EmailConstants.SmtpHost).Value;
                smtpClient.Port = _configurationSection.GetValue<int>(EmailConstants.SmtpPort);
                smtpClient.EnableSsl = _configurationSection.GetValue<bool>(EmailConstants.EnableSsl);
                smtpClient.UseDefaultCredentials = _configurationSection.GetValue<bool>(EmailConstants.UseDefaultCredentials);
                smtpClient.Credentials = new NetworkCredential(_configurationSection.GetSection(EmailConstants.EmailForSending).Value,
                    _configurationSection.GetSection(EmailConstants.EmailPassword).Value);

                await smtpClient.SendMailAsync(mailMessage);
            }
            return true;
        }
    }
}

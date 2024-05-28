using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using NLog;

namespace Lunch_Tinder.Services
{
    public class MailService : IMailService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _configuration;
        private readonly LunchTinderContext _context;

        public MailService(IOptions<MailSettings> mailSettingsOptions, IConfiguration configuration, LunchTinderContext context)
        {
            _mailSettings = mailSettingsOptions.Value;
            _configuration=configuration;
            _context = context;
        }

        /// <summary>
        /// Sends an email 
        /// uses mailtrap
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns>logs error and fails to send</returns>
        public async Task<bool> SendMailAsync(MailData mailData)
        {
            _logger.Info("initiating send mail async method");
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    mailData.EmailToName = "Recipient";
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailAddress);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.HtmlBody = mailData.EmailBody;
                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Send Email");
                return false;
            }
        }

        public async Task<bool> EmailAllUsersOfEventWinner(Event _event, Restaurant winner, LunchGroup lunchgroup)
        {
            LunchGroup? populatedLunchgroup = _context.LunchGroups.Include(u => u.Users).FirstOrDefault(lg => lg.GroupId == lunchgroup.GroupId);
            try
            {
                foreach (User? user in populatedLunchgroup.Users)
                {
                    MailData maildata = new MailData();
                    maildata.EmailAddress = user.EmailAddress;

                    string eventnotificationurl = _configuration.GetValue<string>("mailsettings:eventnotificationurl");
                    string notificationlink = $"{eventnotificationurl}?eventid={Uri.EscapeDataString(_event?.EventId.ToString() ?? string.Empty)}&lunchgroup={Uri.EscapeDataString(lunchgroup.GroupName)}";

                    string template = await LoadEmailTemplateAsync();

                    string emailbody = template.Replace("{{Username}}", user.UserName)
                                               .Replace("{{EventName}}", _event?.Name)
                                               .Replace("{{winner}}", winner.RestaurantName)
                                               .Replace("{{EventStartTime}}", _event.ConvertOffsetToLocalTime(_event.EventStartTime))
                                               .Replace("{{link}}", notificationlink);

                    maildata.EmailSubject = $"Event Winner";
                    maildata.EmailBody = emailbody;
                    await SendMailAsync(maildata);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        
        }


        /// <summary>
        /// reads the content of an email template file and returns the content of the email as a string
        /// </summary>
        /// <returns>template</returns>
        private async Task<string> LoadEmailTemplateAsync()
        {
            string templatePath = "EmailTemplates/EventWinner.html"; // Update with the correct file path
            string template = string.Empty;

            try
            {
                using (var reader = new StreamReader(templatePath))
                {
                    template = await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error loading email template");
            }

            return template;
        }
    }
}

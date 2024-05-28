using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Lunch_Tinder.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ManageUsersController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly LunchTinderContext _context;

        public ManageUsersController(IMailService mailService, IConfiguration _config, LunchTinderContext context)
        {
            _mailService = mailService;
            _configuration = _config;
            _context = context;
        }

        /// <summary>
        /// performs a group join between the Invites and Users tables
        /// retrieves the join results, extracts the InviteId values, and creates a manageViewModel object with the join results, mail data
        /// and a pagination list based on the filtered invites.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            var inviteUserJoins = _context.GetInviteUserJoins();

            var inviteIds = inviteUserJoins.Select(data => data.Invite.InviteId);

            var manageViewModel = new ManageUsersViewModel
            {
                InviteUserJoins = inviteUserJoins,
                MailData = new MailData(),
                Paginations = PaginationList<Invites>.Create(_context.Invites.Where(invite => inviteIds.Contains(invite.InviteId)).AsQueryable(), pageIndex, pageSize)
            };

            return View("~/Views/Admin/ManageUsers.cshtml", manageViewModel);
        }



        /// <summary>
        /// Checks to see if the user exists in the user DB and creates an invite if not
        /// Generates an invitation email with registration link and sends the email 
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendInvitation(MailData mailData)
        {
            // Check if the user exists in the InviteRepo
            Invites? existingInvite = _context.GetInviteByEmail(mailData.EmailAddress ?? string.Empty);

            if (existingInvite != null)
            {
                if (existingInvite.Status == "Pending")
                {
                    TempData["ErrorMessage"] = "User already has an existing invitation.";
                }
                else
                {
                    TempData["ErrorMessage"] = "User already exists.";
                }
            }
            else
            {
                // User doesn't exist in both UserRepository and InviteRepo
                Invites invite = new Invites();
                invite.UserName = "NoUserName";
                invite.EmailAddress = mailData.EmailAddress;
                invite.Status = "Pending";

                _context.AddInvite(invite);

                string registrationUrl = _configuration.GetValue<string>("MailSettings:RegistrationUrl");
                string registrationLink = $"{registrationUrl}?email={Uri.EscapeDataString(mailData.EmailAddress ?? string.Empty)}";


                // Load the email template
                string template = await LoadEmailTemplateAsync();

                string emailBody = template.Replace("{{link}}", registrationLink);

                mailData.EmailSubject = "Registration";
                mailData.EmailBody = emailBody;

                // Send the invitation email using the provided email address
                bool isEmailSent = await _mailService.SendMailAsync(mailData);

                if (isEmailSent)
                {
                    TempData["SuccessMessage"] = "Sent!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to send the invitation email.";
                }
            }

            // Redirect back to the manage users page or another appropriate page
            return RedirectToAction("Index");
        }

        /// <summary>
        /// reads the content of an email template file and returns the content of the email as a string,
        /// logs an error if an exception occurs.
        /// </summary>
        /// <returns>template</returns>
        private async Task<string> LoadEmailTemplateAsync()
        {
            string templatePath = "EmailTemplates/RegistrationEmail.html"; // Update with the correct file path
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
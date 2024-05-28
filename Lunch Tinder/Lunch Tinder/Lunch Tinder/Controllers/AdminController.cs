using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using System.Security.Claims;

namespace Lunch_Tinder.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMailService _mailService;
        private readonly LunchTinderContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(LunchTinderContext lunchtindercontext, IMailService mail, IConfiguration _config)
        {
            _context = lunchtindercontext;
            _mailService = mail;
            _configuration = _config;
        }

        /// <summary>
        /// Displays the events,LunchGroups and Users on the admin index page.
        /// </summary>
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Events = _context.GetAllEvents();
            adminViewModel.LunchGroups = _context.GetAllLunchGroups();
            adminViewModel.Users = _context.GetAllUsers();
            return View("~/Views/Admin/AdminHomePage.cshtml", adminViewModel);
        }

        /// <summary>
        /// Returns the admin view
        /// </summary>
        /// <returns>Admin View</returns>
        public IActionResult Admin()
        {
            return View();
        }


        /// <summary>
        /// retrieves the lunch group details based on the group name
        /// fetches the lunch group and includes related events and users
        /// </summary>
        /// <param name="groupName"></param>
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DisplayGroups(string groupName)
        {
            LunchGroup? group = _context.GetLunchGroupByName(groupName);

            if (group != null)
            {
                LunchGroup? lunchGroup = _context.GetLunchGroup(group.GroupId);

                if (lunchGroup != null)
                {
                    VMLunchEventForGroupModel model = new VMLunchEventForGroupModel();
                    model.Group = group;
                    model.RestaurantOptions = _context.Restaurants.ToList();
                    model.Group.Events = model.Group.Events.Where(x => x.EventStartTime.DateTime > DateTime.UtcNow).ToList();

                    return View("~/Views/User/LunchGroupDetails.cshtml", model);
                }
            }

            _logger.Error($"Lunch group '{groupName}' not found.");
            // Handle the case when the group or lunchGroup is null
            return NotFound();

        }

        /// <summary>
        /// Updates the description in the current lunch group and saves changes 
        /// </summary>
        /// <param name="lunchgroup"></param>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult SaveGroupDescription(VMLunchEventForGroupModel model)
        {
            _context.UpdateGroupDescription(model);

            return RedirectToAction("DisplayGroups", "Admin", new { groupName = model.Group.GroupName });
        }

        /// <summary>
        /// Returns the Manage User View
        /// </summary>
        public IActionResult ManageUser()
        {
            return View("~/Views/Admin/ManageUsers.cshtml");
        }

        /// <summary>
        /// handles and displays errors during a http request
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Gets the group id and returns the invite to lunch group according to the ID
        /// </summary>
        /// <param name="groupid"></param>
        [Authorize(Roles = "ADMIN")]
        public IActionResult InviteToLunchGroup(int groupid)
        {
            VMInviteLG Invite = new VMInviteLG();

            Invite.Group = _context.LunchGroups.FirstOrDefault(g => g.GroupId == groupid);

            return PartialView("PartialView/InviteToLunchGroup", Invite);
        }

        /// <summary>
        /// Checks to see if there are any matches for the UserName and Email
        /// </summary>
        /// <param name="usernameemail"></param>
        public IActionResult CheckUsernameEmail(string emailusername)
        {
            bool userExists = _context.Users.Any(u => u.UserName == emailusername || u.EmailAddress == emailusername);
            
            return Json(new { userExists });
        }

        /// <summary>
        /// invites a user to join a lunch group
        /// when invited, sets the status as pending and checks to see if the @ symbol is present
        /// loads the email template with the email invitation link and lunch group name
        /// </summary>
        /// <param name="InviteToLG"></param>
        [HttpPost]
        public async Task<IActionResult> InviteToLunchGroup(VMInviteLG InviteToLG)
        {
            InviteToLunchGroup invite = new InviteToLunchGroup();

            ClaimsPrincipal claimuser = HttpContext.User;

            User user = _context.GetUserById(Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            invite.LunchGroupName = InviteToLG?.Group?.GroupName;
            invite.UsernameEmail = InviteToLG?.UserNameEmail;
            invite.Status = "Pending";

            string UserEmail = InviteToLG?.UserNameEmail ?? string.Empty;

            if (!invite.UsernameEmail.Contains("@"))
            {
                UserEmail = _context.GetEmailByUsername(UserEmail);
                invite.UsernameEmail = UserEmail;
            }

            _context.AddInviteToLunchGroup(invite);

            MailData maildata = new MailData();
            maildata.EmailAddress = UserEmail;

            string invitationurl = _configuration.GetValue<string>("mailsettings:invitelgurl");
            string invitationlink = $"{invitationurl}?email={Uri.EscapeDataString(UserEmail ?? string.Empty)}&lunchgroup={Uri.EscapeDataString(InviteToLG.Group.GroupName ?? string.Empty)}";

            //load the email template
            string template = await LoadEmailTemplateAsync();

            string emailbody = template.Replace("{{link}}", invitationlink)
                                       .Replace("{{lunchgroup}}", invite.LunchGroupName)
                                       .Replace("{{admin}}",user.UserName);

            maildata.EmailSubject = $"Invite to {invite.LunchGroupName}";
            maildata.EmailBody = emailbody;

            //send the invitation email using the provided email address
            bool isemailsent = await _mailService.SendMailAsync(maildata);

            return RedirectToAction("DisplayGroups", "Admin", new { groupName = invite.LunchGroupName });
        }

        /// <summary>
        /// reads the content of an email template file and returns the content of the email as a string
        /// </summary>
        /// <returns>template</returns>
        private async Task<string> LoadEmailTemplateAsync()
        {
            string templatePath = "EmailTemplates/LunchGroupInvite.html"; // Update with the correct file path
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
                _logger.Error("Error loading email template", ex);
            }

            return template;
        }

        /// <summary>
        /// Gets the restaurant according to the group id and put them into a list
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns>Create lunch event for group</returns>
        public IActionResult GetRestaurants(int groupid)
        {
            VMLunchEventForGroupModel LunchEvent = new VMLunchEventForGroupModel();
            LunchEvent.Group = _context.LunchGroups.FirstOrDefault(g => g.GroupId == groupid);
            LunchEvent.RestaurantOptions = _context.Restaurants.ToList(); ;

            return PartialView("PartialView/CreateLunchEventForGroup", LunchEvent);
        }
    }
}

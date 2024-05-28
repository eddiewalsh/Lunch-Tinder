using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using System.Security.Claims;

namespace Lunch_Tinder.Controllers
{
    [Authorize]
    public class LGUController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly LunchTinderContext _context;

        public LGUController(LunchTinderContext lunchtindercontext)
        {
            _context = lunchtindercontext;
        }

        /// <summary>
        /// Renders the index view of the LunchGroupUser controller, displaying relevant information for the authenticated user.
        /// </summary>
        /// <returns>The index view with the LunchGroupUserHomePageViewModel containing user's lunch groups and events.</returns>
        [Authorize(Roles = "USER")]
        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            int userId = Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            LGUViewModel lguViewModel = new LGUViewModel();

            User? user = _context.GetUserWithLunchGroups(userId);

            if (user != null)
            {
                lguViewModel.LunchGroups = user.LunchGroups?.ToList();
                lguViewModel.Events = _context.GetAllEventsBasedOnUser(user);
            }

            return View("~/Views/LunchGroupUser/LGUHomePage.cshtml", lguViewModel);
        }

        /// <summary>
        /// Displays the details of a lunch group with the given group name.
        /// </summary>
        /// <param name="groupName">The name of the lunch group.</param>
        /// <returns>The view displaying the lunch group details.</returns>
        [Authorize(Roles = "USER")]
        public IActionResult ViewLunchGroup(string groupName)
        {
            LunchGroup? group = _context.GetLunchGroupByName(groupName);
            
            group = _context.GetLunchGroupWithRelatedEntities(group.GroupId);

            group.Events = group.Events.Where(x => x.EventStartTime.DateTime > DateTime.UtcNow).ToList();

            MaskEmails(group?.Users);

            return View("~/Views/LunchGroupUser/ViewLunchGroupDetailsLGU.cshtml", group);
        }

        /// <summary>
        /// masks the email of the user when displayed
        /// </summary>
        /// <param name="users"></param>
        private void MaskEmails(IEnumerable<User>? users)
        {
            if (users != null)
            {
                foreach (var user in users)
                {
                    user.EmailAddress = MaskEmail(user?.EmailAddress ?? string.Empty);
                }
            }
        }

        /// <summary>
        /// Masks the email address by replacing characters before the '@' symbol with asterisks, except for the first character.
        /// </summary>
        /// <param name="email">The email address to mask.</param>
        /// <returns>The masked email address.</returns>
        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            int atIndex = email.IndexOf('@');
            if (atIndex < 2)
            {
                return email;
            }

            string maskedPart = new string('*', atIndex - 2);
            string domainPart = email.Substring(atIndex);
            return maskedPart + email.Substring(atIndex - 1, 1) + domainPart;
        }

        /// <summary>
        /// Handles the error scenario and renders the error view.
        /// </summary>
        /// <returns>The IActionResult representing the error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Logs out the user by signing out the authentication scheme and redirecting to the login page.
        /// </summary>
        /// <returns>The task representing the asynchronous logout operation.</returns>
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}

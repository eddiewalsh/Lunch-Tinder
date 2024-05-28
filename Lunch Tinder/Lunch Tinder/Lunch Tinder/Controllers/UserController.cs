using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Security;
using Lunch_Tinder.Services;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Security.Claims;

namespace Lunch_Tinder.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly LunchTinderContext _context;

        /// <summary>
        /// Initializes our UserRepository
        /// </summary>
        public UserController(IConfiguration config, IMailService mailservice, LunchTinderContext lunchtindercontext)
        {
            this._configuration = config;
            this._mailService = mailservice;
            this._context = lunchtindercontext;
        }

        /// <summary>
        ///  Retrieves the current user from the HttpContext and checks if the user is authenticated.
        ///  If so, then we redirect to the LGU index else we return to the view
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ClaimsPrincipal claimuser = HttpContext.User;

            ViewData["returnUrl"] = returnUrl;

            if (claimuser?.Identity?.IsAuthenticated ?? false)
            {
                if (claimuser.IsInRole("USER"))
                {
                    return RedirectToAction("Index", "LGU");
                }
                else if (claimuser.IsInRole("ADMIN"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View("Login");
        }

        /// <summary>
        /// We verify the LoginCredentials, if these credentials are correct, than we get the user by login details. 
        /// We create a list of claims assosciated with signed in user. We use cookies for authentication a redirect our user to the 
        /// Index in LGUController, else the login page is returned with a validation message
        /// </summary>
        /// <param name="modelLogin">the View Model used</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            if (string.IsNullOrEmpty(modelLogin.EmailAddress))
            {
                ViewData["ValidateMessage"] = "Incorrect email or password";
                return View();
            }

            if (string.IsNullOrEmpty(modelLogin.Password))
            {
                ViewData["ValidateMessage"] = "Incorrect email or password";
                return View();
            }

            if (_context.VerifyLoginCredentials(modelLogin.EmailAddress.Trim(), modelLogin.Password.Trim()))
            {
                User? user = _context.GetUserByEmail(modelLogin.EmailAddress);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.UserType ?? string.Empty)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                if(modelLogin.returnUrl is not null)
                {
                    return Redirect(modelLogin.returnUrl);
                }



                if (user?.UserType?.Equals("USER") == true)
                {
                    return RedirectToAction("Index", "LGU");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }

            }

            ViewData["ValidateMessage"] = "Incorrect email or password";

            return View();
        }

        /// <summary>
        /// This creates a ClaimsPrincipal and uses the name identifier to retrieve the user by GetID().
        /// Once retrieved, we return the Profile View along with our user
        /// </summary>
        public IActionResult Profile()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            int userId = Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            VMProfile profile = new VMProfile();

            profile.User = _context?.GetUserById(userId) ?? new User();

            if (profile.User == null)
            {
                return NotFound();
            }

            return View("Profile", profile);
        }

        /// <summary>
        /// Verifies the username is unique and checks if the username is null or empty. If the user name is unique
        /// and not empty, we get our name identifier and use it to get our user by the ID. We assign the new username value to our user and save changes
        /// and send a validation message
        /// </summary>
        /// <param name="editeduser">The user details that has been edited</param>
        [HttpPost]
        public IActionResult Profile(VMProfile editeduser)
        {
            if (!_context.VerifyUserNameIsUnique(editeduser.User?.UserName ?? string.Empty))
            {
                ViewData["ErrorMessage"] = "Username is taken";
                return View("Profile", editeduser);

            }
            else if (editeduser?.User?.UserName?.IsNullOrEmpty() == true)
            {
                ViewData["ErrorMessage"] = "Must enter a username";
                return View("Profile", editeduser);
            }
            else
            {
                ClaimsPrincipal claimuser = HttpContext.User;

                int userId = Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value);


                User user = _context?.GetUserById(userId) ?? new User();

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = editeduser?.User?.UserName;

                _context?.UpdateUser(user);

                ViewData["ValidateMessage"] = "Username successfuly changed";
                return View("Profile", editeduser);
            }
        }

        /// <summary>
        /// Verifies the password input in not null and checks the password against the stored password and returns the appropiate view
        /// </summary>
        /// <param name="edituser"></param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(VMProfile edituser)
        {
            if (string.IsNullOrWhiteSpace(edituser.NewPassword))
            {
                ViewData["ErrorMessage"] = "New password cannot be empty";
                return View("Profile", edituser);
            }

            if(string.IsNullOrWhiteSpace(edituser.ConfirmNewPassword))
            {
                ViewData["ErrorMessage"] = "Confirm your new password";
                return View("Profile", edituser);
            }

            if(edituser.ConfirmNewPassword != edituser.NewPassword)
            {
                ViewData["ErrorMessage"] = "New password does not match confirmation password";
                return View("Profile", edituser);
            }

            User? user = _context.GetUserById(edituser.User.UserId);
            if (PasswordHelper.VerifyPassword(edituser.OldPassword ?? string.Empty, user?.Password ?? string.Empty))
            {
                user.Password = PasswordHelper.HashPassword(edituser.NewPassword ?? string.Empty);
                _context.UpdateUser(user);
                ViewData["ValidateMessage"] = "Password successfuly changed";

                MailData mailData = new MailData();
                mailData.EmailAddress = user.EmailAddress;
                mailData.EmailSubject = "Password Change Confirmation";
                string template = await LoadEmailTemplateAsync("EmailTemplates/PasswordChangeEmail.html");

                string emailBody = template
                                   .Replace("{{Username}}", user.UserName);

                mailData.EmailBody = emailBody;

                await _mailService.SendMailAsync(mailData);

                return View("Profile", edituser);
            }
            else
            {
                ViewData["ErrorMessage"] = "Incorrect password";
                return View("Profile", edituser);
            }
        }

        /// <summary>
        ///  Creates an instance of the view model "VMRegistration" and returns the Registration view along
        ///  with our view model
        /// </summary>
        [AllowAnonymous]
        public IActionResult Registration([FromQuery] string email)
        {
            VMRegistration vmRegister = new VMRegistration();
            vmRegister.EmailAddress = email;
            if (_context.EmailAlreadyInUse(vmRegister.EmailAddress ?? string.Empty))
            {
                ViewData["ValidateMessage"] = "Email already in use";
                return View("Registration", vmRegister);
            }

            return View("Registration", vmRegister);
        }

        /// <summary>
        /// Create a new instance of user, assign our values and add to our repository. We than return to our login view
        /// </summary>
        /// <param name="vmRegister">the view model that contains the registration details</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registration(VMRegistration vmRegister)
        {

            if (_context.EmailAlreadyInUse(vmRegister.EmailAddress ?? string.Empty))
            {
                ViewData["ValidateMessage"] = "Email already in use";
                return View("Registration", vmRegister);
            }

            User user = new User();
            user.UserName = vmRegister.UserName;
            user.EmailAddress = vmRegister.EmailAddress;
            user.Password = PasswordHelper.HashPassword(vmRegister.Password ?? string.Empty);
            user.UserType = "USER";

            _context.AddUser(user);

            // Check if the user exists in the Invites repository
            Invites? existingInvite = _context.GetInviteByEmail(vmRegister.EmailAddress ?? string.Empty);
            if (existingInvite != null && existingInvite.Status == "Pending")
            {
                existingInvite.UserName = vmRegister.UserName;
                existingInvite.Status = "Accepted";
                _context.UpdateInvite(existingInvite);
            }

            MailData mailData = new MailData();
            mailData.EmailAddress = vmRegister.EmailAddress;
            mailData.EmailSubject = "Confirmation";
            string loginUrl = _configuration.GetValue<string>("MailSettings:LoginPageUrl");
            string template = await LoadEmailTemplateAsync("EmailTemplates/ConfirmationEmail.html");

            string emailBody = template
                .Replace("{{link}}", loginUrl)
                .Replace("{{Username}}", vmRegister.UserName);

            mailData.EmailBody = emailBody;

            bool isEmailSent = await _mailService.SendMailAsync(mailData);
            if (isEmailSent)
            {
                return View("Login");
            }

            return View(vmRegister.EmailAddress);
        }

        /// <summary>
        /// Loads our Confirmation Email
        /// </summary>
        private async Task<string> LoadEmailTemplateAsync(string emailtemplate)
        {
            string templatePath = emailtemplate; // Update with the correct file path
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

        /// <summary>
        /// We check if our email is already registered function and send a JSON repsonse for our javascript function
        /// </summary>
        /// <param name="username">the target email</param>
        public IActionResult CheckEmail(string email)
        {
            bool isEmailTaken = IsEmailTaken(email);
            return Json(new { isEmailTaken });
        }


        /// <summary>
        /// We call our VerifyUsernameIsUnique function and send a JSON repsonse for our javascript function
        /// </summary>
        /// <param name="username">the target email</param>
        public IActionResult CheckUsername(string username)
        {
            bool isUsernameTaken = !(_context.VerifyUserNameIsUnique(username));
            return Json(new { isUsernameTaken });
        }

        /// <summary>
        ///  Verifies if the email address is in the user table
        /// </summary>
        /// <param name="email">target email</param>
        public bool IsEmailTaken(string email)
        {
            return _context.Users.Any(user => user.EmailAddress == email);
        }

        /// <summary>
        /// The user can accept a lunch invite once they click on the invitation link,
        /// logs the user in, redirects them to the lunch group page and adds them as a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="lunchgroup"></param>
        /// <returns></returns>
        public async Task<IActionResult> AcceptLunchGroupInvite([FromQuery] string email, [FromQuery] string lunchgroup)
        {
            User? user = _context?.GetUserByEmail(email);
            LunchGroup? lunchGroup = _context?.GetLunchGroupByName(lunchgroup);
            InviteToLunchGroup? invitetoLG = _context?.GetInviteByEmailAndLunchGroup(email,lunchgroup);
			var useridClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

			User? loggedinuser = _context.GetUserById(Convert.ToInt32(useridClaim.Value));

            if(loggedinuser.EmailAddress != user.EmailAddress) 
            {
				if (loggedinuser.UserType.Equals("USER"))
				{
					return RedirectToAction("Index", "LGU");
				}
				else
				{
					return RedirectToAction("Index", "Admin");
				}
			}

            if (!lunchGroup.Users.Any(x => x.EmailAddress == loggedinuser.EmailAddress))
            {
                _context.AddUserToLunchGroup(user, lunchGroup);
            }

			invitetoLG.Status = "Accepted";
			_context?.InvitesLG.Update(invitetoLG);
            _context?.SaveChanges();


            if (loggedinuser.UserType.Equals("USER"))
            {
                return RedirectToAction("ViewLunchGroup", "LGU", new { groupName = lunchGroup?.GroupName });
            }
            else
            {
                return RedirectToAction("DisplayGroups", "Admin", new { groupName = lunchGroup?.GroupName });
            }
        }
    }
}

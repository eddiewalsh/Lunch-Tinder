using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Security.Claims;

namespace Lunch_Tinder.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly LunchTinderContext _context;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public EventController(LunchTinderContext lunchtindercontext, IMailService mailService, IConfiguration config)
        {
            _context = lunchtindercontext;
            _mailService = mailService;
            _configuration = config;
        }

        /// <summary>
        /// Changes the Event vote date and updates the Correspoding dates in the db
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
		[HttpPost]
        public async Task<IActionResult> EditEvent(VMEventPage model)
        {

            model.selectedEvent = _context.GetEventById(model.selectedEvent.EventId);

            if (model.selectedEvent != null)
            {

                DateTime date = model.EditedVoteEndDate.Date;

                _logger.Info($"BEFORE::EVENT START DATE TIME: {model.selectedEvent.EventStartTime}");
                _logger.Info($"BEFORE::EVENT END DATE TIME: {model.selectedEvent.EventEndTime}");
                _logger.Info($"BEFORE::VOTE START DATE TIME: {model.selectedEvent.VotingStartTime}");
                _logger.Info($"BEFORE::VOTE END DATE TIME: {model.selectedEvent.VotingEndTime}");

                model.selectedEvent.EventStartTime = new DateTimeOffset(date.Year, date.Month, date.Day,model.selectedEvent.EventStartTime.Hour,
                    model.selectedEvent.EventStartTime.Minute,0,model.selectedEvent.EventStartTime.Offset);                
                
                model.selectedEvent.EventEndTime = new DateTimeOffset(date.Year, date.Month, date.Day,model.selectedEvent.EventEndTime.Hour,
                    model.selectedEvent.EventEndTime.Minute,0,model.selectedEvent.EventEndTime.Offset);
                
                //model.selectedEvent.VotingStartTime = new DateTimeOffset(date.Year, date.Month, date.Day, model.selectedEvent.VotingStartTime.Hour,
                //    model.selectedEvent.VotingStartTime.Minute, 0, model.selectedEvent.VotingStartTime.Offset);
                
                model.selectedEvent.VotingEndTime = new DateTimeOffset(date.Year, date.Month, date.Day, model.selectedEvent.VotingEndTime.Hour,
                    model.selectedEvent.VotingEndTime.Minute, 0, model.selectedEvent.VotingEndTime.Offset);


                _logger.Info($"AFTER::EVENT START DATE TIME: {model.selectedEvent.EventStartTime}");
                _logger.Info($"AFTER::EVENT END DATE TIME: {model.selectedEvent.EventEndTime}");
                _logger.Info($"AFTER::VOTE START DATE TIME: {model.selectedEvent.VotingStartTime}");
                _logger.Info($"AFTER::VOTE END DATE TIME: {model.selectedEvent.VotingEndTime}");

                _context.UpdateEvent(model.selectedEvent);
                return RedirectToAction("DisplayEvent", "Event", new { eventId = model.selectedEvent.EventId });
            }
            else
            {
                // Event not found, handle the error case accordingly
                return NotFound();
            }
        }


        /// <summary>
        /// Creates a new event for a lunch group based on the provided model.
        /// </summary>
        /// <param name="model">The model containing the details of the event to be created.</param>
        /// <returns>The result of the action.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvent(VMLunchEventForGroupModel model)
        {
            // Retrieve the selected venues from the form data
            string selectedVenuesString = Request.Form["SelectedVenues"];
            string[] selectedVenues = selectedVenuesString.Split(',');

            LunchGroup? lunchgroup = _context.GetLunchGroupByName(model.Group.GroupName);

            model.EventStartDateTime = model.EventStartDateTime;

            Event newEvent = new Event
            {
                Name = "Staff Lunch",
                Description = $"Event at {selectedVenuesString}",
                EventStartTime = model.EventStartDateTime,
                EventEndTime = model.VoteEndDateTime,
                Status = "Open",
                VenueWinner = " ",
                VotingStartTime = model.VoteStartDateTime,
                VotingEndTime = model.VoteEndDateTime
            };

            if (selectedVenues != null && selectedVenues.Length > 0)
            {
                foreach (string venue in selectedVenues)
                {
                    Restaurant? restaurant = _context.GetRestaurantByName(venue);
                    
                    if(!_context.ChecksEventForDuplicateRestaurantOptions(newEvent, restaurant))
                    {
                        newEvent?.RestaurantOptions?.Add(restaurant);
                    }
                }
            }

            _context.Events.Add(newEvent);
            lunchgroup?.Events.Add(newEvent);

            _context.LunchGroups.Update(lunchgroup);
            _context.SaveChanges();

            foreach (User? user in lunchgroup.Users ?? new List<User>())
            {
                MailData maildata = new MailData();
                maildata.EmailAddress = user.EmailAddress;

                string eventnotificationurl = _configuration.GetValue<string>("mailsettings:eventnotificationurl");
                string notificationlink = $"{eventnotificationurl}?eventid={Uri.EscapeDataString(newEvent?.EventId.ToString() ?? string.Empty)}&lunchgroup={Uri.EscapeDataString(lunchgroup.GroupName)}";

                string template = await LoadEmailTemplateAsync();

                string emailbody = template.Replace("{{link}}", notificationlink)
                                           .Replace("{{Username}}", user.UserName)
                                           .Replace("{{eventname}}", newEvent?.Name)
                                           .Replace("{{startdate}}", newEvent?.ConvertOffsetToLocalTime(newEvent.VotingStartTime))
                                           .Replace("{{enddate}}", newEvent?.ConvertOffsetToLocalTime(newEvent.VotingEndTime))
                                           .Replace("{{eventstart}}", newEvent?.ConvertOffsetToLocalTime(newEvent.EventStartTime))
                                           .Replace("{{venues}}", selectedVenuesString)
                                           .Replace("{{Lunchgroup}}", lunchgroup.GroupName);

                maildata.EmailSubject = $"New Event";
                maildata.EmailBody = emailbody;
                await _mailService.SendMailAsync(maildata);
            }

            return RedirectToAction("DisplayGroups", "Admin", new { groupName = lunchgroup.GroupName });
        }

        /// <summary>
        /// reads the content of an email template file and returns the content of the email as a string
        /// </summary>
        /// <returns>template</returns>
        private async Task<string> LoadEmailTemplateAsync()
        {
            string templatePath = "EmailTemplates/EventNotification.html"; // Update with the correct file path
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
        /// displays the selected event
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public IActionResult DisplayEvent(int eventid)
        {
            VMEventPage voteviewmodel = new VMEventPage();
            ClaimsPrincipal claimuser = HttpContext.User;
            int userid = Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            voteviewmodel.selectedEvent = _context.GetEventById(eventid);
            voteviewmodel.user = _context.GetUserById(userid);

            if (voteviewmodel.selectedEvent.VotingStartTime.DateTime <= DateTime.UtcNow && voteviewmodel.selectedEvent.VotingEndTime.DateTime >= DateTime.UtcNow)
            {
                voteviewmodel.vote = _context.GetVoteByUserAndEvent(userid, eventid);
                ViewData["VotingStatus"] = "Voting In Progress";
                ViewData["Winner"] = "";
            }
            else if (voteviewmodel.selectedEvent.VotingStartTime.DateTime > DateTime.UtcNow)
            {
                ViewData["VotingStatus"] = $"Voting will commence on {voteviewmodel.selectedEvent.ConvertOffsetToLocalTime(voteviewmodel.selectedEvent.VotingStartTime)}";
                ViewData["Winner"] = "";
            }
            else if (voteviewmodel.selectedEvent.VotingEndTime.DateTime < DateTime.UtcNow)
            {
                ViewData["VotingStatus"] = "Voting Closed";
                ViewData["Winner"]= $"{voteviewmodel?.selectedEvent?.VenueWinner}";
            }


            return View("~/Views/LunchGroupUser/LunchGroupEvent.cshtml", voteviewmodel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">the email of the user</param>
        /// <param name="eventid">the eventid of the new event</param>
		public async Task<IActionResult> ViewEventFromEmail([FromQuery] string eventid, [FromQuery] string lunchgroup)
        {
            Event? newEvent = _context?.GetEventById(Convert.ToInt32(eventid));
            var useridClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            User user = _context.GetUserById(Convert.ToInt32(useridClaim.Value));
            LunchGroup? lunchGroup = _context.GetLunchGroupByName(lunchgroup);

            if (_context.VerifyUserIsLunchGroupMember(lunchGroup, user))
            {
                return RedirectToAction("DisplayEvent", "Event", new { eventid = newEvent?.EventId });
            }
            else
            {
                if (user?.UserType?.Equals("USER") == true)
                {
                    return RedirectToAction("Index", "LGU");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
        }

        [HttpPost]
        public ActionResult VoteInEvent(int eventid, int restaurantid)
        {
            Vote vote = new Vote();
            ClaimsPrincipal claimuser = HttpContext.User;
            VMEventPage voteviewmodel = new VMEventPage();

            vote.EventVoteID = eventid;
            vote.UserVoteID = Convert.ToInt32(claimuser.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            vote.RestaurantVoteID = restaurantid;

            if (_context.VerifyVoteExists(vote.UserVoteID, vote.EventVoteID))
            {
                Vote? existingvote = _context.GetVoteByUserAndEvent(vote.UserVoteID, vote.EventVoteID);
                existingvote.RestaurantVoteID = restaurantid;
                _context.UpdateVote(existingvote);
                ViewData["VoteSuccess"] = "Vote Added";
                voteviewmodel.vote = existingvote;
                return Ok();
            }
            else
            {
                _context.AddVote(vote);
                Event _event = _context.GetEventById(vote.EventVoteID);
                _event?.Votes?.Add(vote);
                _context.UpdateEvent(_event);
                voteviewmodel.vote = vote;
                return Ok();
            }
        }
    }
}

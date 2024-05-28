using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Lunch_Tinder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Data;

namespace Lunch_Tinder.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ManageGroupsController : Controller
    {
        private readonly LunchTinderContext _context;

        public ManageGroupsController(LunchTinderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the lunch groups and populates the list in the view
        /// </summary>
        /// <returns>Manage Group View and model</returns>
        public IActionResult Index()
        {
            ManageGroupsViewModel manageGroupsViewModel = new ManageGroupsViewModel();

            manageGroupsViewModel.LunchGroups = _context.GetAllLunchGroups();

            return View("~/Views/Admin/ManageGroups.cshtml", manageGroupsViewModel);
        }

        /// <summary>
        /// Creates a new lunch group with the provided group name and description and adds it to the database.
        /// After submitting, it redirects to the lunch group details page.
        /// If the "description" field is empty, it will be submitted as an empty string.
        /// </summary>
        /// <param name="groupName">The name of the lunch group.</param>
        /// <param name="description">The description of the lunch group.</param>>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string groupName, string description)
        {
            // Create a new LunchGroup instance
            LunchGroup lunchGroup = new LunchGroup
            {
                GroupName = groupName,
                Description = description ?? string.Empty
            };

            _context.AddLunchGroup(lunchGroup);

            string group = lunchGroup.GroupName;

            // Redirect to the lunch group details page
            return RedirectToAction("DisplayGroups", "Admin", new { groupName = group });

        }
    }
}

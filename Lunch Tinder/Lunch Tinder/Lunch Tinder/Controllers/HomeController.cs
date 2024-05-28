using Lunch_Tinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;

namespace Lunch_Tinder.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public HomeController() { }

        /// <summary>
        /// returns the Home index page
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            _logger.Info("Index action executed.");
            return View();
        }

        /// <summary>
        /// returns the Privacy page
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Privacy()
        {
            _logger.Info("Privacy action executed.");
            return View();
        }

        /// <summary>
        /// returns the Error page
        /// </summary>
        /// <returns>View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.Info("Error action executed.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
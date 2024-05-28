using Lunch_Tinder.Data;
using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunch_Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LunchTinderController : ControllerBase
    {

        public static LunchTinderContext? _context;
        private readonly IMailService _mailService;

        public LunchTinderController(LunchTinderContext lunchtindercontext, IMailService mailService)
        {
            _context = lunchtindercontext;
            _mailService = mailService;
        }

        // GET: api/<LunchTinderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Event>? _events = _context.GetEventsThatAreClosed();
            bool success = false;
            if (_events == null || _events.Count == 0)
            {
                return Ok("No event found that is closed");
            }
            else
            {
                foreach (Event _eventt in _events)
                {
                    Restaurant? winner = _context.CalculateEventWinner(_eventt);



                    foreach (LunchGroup lg in _eventt.LunchGroups)
                    {
                        success = await _mailService.EmailAllUsersOfEventWinner(_eventt, winner, lg);
                    }

                    _context.SetEventStatusToClosed(_eventt);
                    _context.SetEventWinner(_eventt, winner.RestaurantName);
                }

                if (success)
                {
                    return Ok("Emails sent successfully");
                }
                else
                {
                    return Ok("Error sending emails");
                }
            }
        }

        // GET api/<LunchTinderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LunchTinderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LunchTinderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LunchTinderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

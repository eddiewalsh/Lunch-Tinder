using Lunch_Tinder.Models;
using Lunch_Tinder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lunch_Tinder.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;

        private readonly IMailService _mailService;

        public MailController(IMailService mailService, ILogger<MailController> logger)
        {
            _mailService = mailService;
            _logger = logger;
            _logger.LogInformation("MailController has been constructed");
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            _logger.LogInformation("SendMail has been called");
            return await _mailService.SendMailAsync(mailData);
        }
    }
}

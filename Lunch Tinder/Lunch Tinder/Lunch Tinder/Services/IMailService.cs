using Lunch_Tinder.Models;

namespace Lunch_Tinder.Services
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(MailData mailData);

        Task<bool> EmailAllUsersOfEventWinner(Event _event, Restaurant winner, LunchGroup lunchgroup);
    }
}


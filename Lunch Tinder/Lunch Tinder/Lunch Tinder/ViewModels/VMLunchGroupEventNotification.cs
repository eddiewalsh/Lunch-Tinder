using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
    public class VMLunchGroupEventNotification
    {
        public string? venues { get; set; }

        public Event? newEvent { get; set; }

        public LunchGroup? lunchgroup { get; set; }
    }
}

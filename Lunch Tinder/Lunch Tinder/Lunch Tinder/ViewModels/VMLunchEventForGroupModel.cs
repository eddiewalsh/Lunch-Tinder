using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
    public class VMLunchEventForGroupModel
    {
        public LunchGroup? Group { get; set; }
        public List<Restaurant>? RestaurantOptions { get; set; }
        public Event? Event { get; set; }
        public List<string>? SelectedVenues { get; set; }
        public DateTime EventStartDate { get; set; }
        public TimeSpan EventStartTime { get; set; }
        public DateTime VotingEndDate { get; set; }
        public TimeSpan VotingCloseTime { get; set; }
        public DateTimeOffset EventStartDateTime { get; set; }
        public DateTimeOffset EventEndDateTime { get; set; }
        public DateTimeOffset VoteStartDateTime { get; set; }
        public DateTimeOffset VoteEndDateTime { get; set; }
    }
}

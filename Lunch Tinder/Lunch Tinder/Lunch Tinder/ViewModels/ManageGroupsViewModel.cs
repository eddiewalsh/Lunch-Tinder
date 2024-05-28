using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
    public class ManageGroupsViewModel
    {
        public List<LunchGroup>? LunchGroups { get; set; }
        public List<User>? Users { get; set; }
        public List<Event>? Events { get; set; }
    }
}

using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
	public class VMEventPage
	{
		public Event? selectedEvent { get; set; }
		public LunchGroup? group { get; set; }
		public Vote? vote { get; set; }
		public Event? Event { get; set; }
		public User? user { get; set; }
		public List<Restaurant>? venueoptions { get; set; }
		public DateTimeOffset EditedVoteEndDate { get; set; }
	}
}
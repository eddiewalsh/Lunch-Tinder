using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTimeOffset EventStartTime { get; set; }

        public DateTimeOffset EventEndTime { get; set; }

        [Required]
        public DateTimeOffset VotingStartTime { get; set; }

        [Required]
        public DateTimeOffset VotingEndTime { get; set; }

        public string Status { get; set; }

        public string VenueWinner { get; set; }

        public virtual ICollection<LunchGroup>? LunchGroups { get; set; }

        public virtual ICollection<Restaurant>? RestaurantOptions { get; set; }

        public virtual ICollection<Vote>? Votes { get; set; }

        public Event()
        {
            RestaurantOptions = new List<Restaurant>();
            LunchGroups = new List<LunchGroup>();
            Votes = new List<Vote>();
        }

        public string ConvertOffsetToLocalTime(DateTimeOffset utcDateTime)
        {
            DateTime localDateTime = utcDateTime.DateTime + utcDateTime.Offset;

            return localDateTime.ToString("dd/MM/yyyy hh:mm tt");
        }

    }
}

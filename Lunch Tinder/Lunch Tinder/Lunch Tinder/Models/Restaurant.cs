using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantID { get; set; }

        [Required]
        public string? RestaurantName { get; set; }

        [Required]
        public string? RestaurantDescription { get; set; }

        public virtual ICollection<Event>? Events { get; set; }

        public Restaurant()
        {
            Events = new List<Event>();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class LunchGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }

        [Required]
        public string? GroupName { get; set; }

        [Required]
        public string? Description { get; set; }

        public virtual ICollection<User>? Users { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public LunchGroup()
        {
            Users = new List<User>();
            Events = new List<Event>();
        }

    }
}

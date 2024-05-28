using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string? EmailAddress { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? UserType { get; set; }

        public virtual ICollection<LunchGroup>? LunchGroups { get; set; }

        public virtual ICollection<Vote> Vote { get; set; }

        public User()
        {
            LunchGroups = new List<LunchGroup>();
            Vote = new List<Vote>();
        }
    }
}

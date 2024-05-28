using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class InviteToLunchGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InviteID { get; set; }

        [Required]
        public string? LunchGroupName { get; set; }

        [Required]
        public string? UsernameEmail { get; set; }

        [Required]
        public string? Status { get; set; }
    }
}

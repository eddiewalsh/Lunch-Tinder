using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class Invites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InviteId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? EmailAddress { get; set; }

        public string? Status { get; set; }

    }
}

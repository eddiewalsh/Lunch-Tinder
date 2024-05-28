using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch_Tinder.Models
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }

        [Required, ForeignKey("User")]
        public int UserVoteID { get; set; }
        public virtual User? User { get; set; }

        [Required, ForeignKey("Restaurant")]
        public int RestaurantVoteID { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        [Required, ForeignKey("Event")]
        public int EventVoteID { get; set; }
        public virtual Event? Event { get; set; }
    }
}

using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
    public class ManageUsersViewModel
    {
        public List<InviteUserJoin>? InviteUserJoins { get; set; }
        public MailData? MailData { get; set; }
        public PaginationList<Invites>? Paginations { get; set; }
    }
}

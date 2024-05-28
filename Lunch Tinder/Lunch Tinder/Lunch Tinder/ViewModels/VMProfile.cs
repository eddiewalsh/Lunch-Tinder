using Lunch_Tinder.Models;

namespace Lunch_Tinder.ViewModels
{
    public class VMProfile
    {
        public User? User { get; set; }

        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmNewPassword { get; set; }
    }
}

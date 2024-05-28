using Lunch_Tinder.Models;

namespace Lunch_Tinder.Data
{
    public partial class LunchTinderContext
    {
        /// <summary>
        /// adds the invite details to the Db
        /// logs an error if there are issues
        /// </summary>
        /// <param name="item"></param>
        public void AddInvite(Invites item)
        {
            try
            {
                Invites.Add(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add invitation");
            }
        }

        public void DeleteInvite(Invites item)
        {
            _logger.Error("not implmented");
        }

        /// <summary>
        /// gets all the invite details from the Db
        /// logs an error if there are issues
        /// </summary>
        /// <returns>Empty list and logs error</returns>
        public List<Invites> GetAllInvites()
        {
            try
            {
                return Invites.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get all invitations");
                return new List<Invites>();
            }
        }

        /// <summary>
        /// gets all the invite details from the Db by ID
        /// logs an error if there are issues
        /// </summary>
        /// <param name="id"></param>
        /// <returns>if not found, returns a special value to indicate that there is an absence of an invitation </returns>
        public Invites? GetInviteById(int id)
        {
            try
            {
                return Invites.FirstOrDefault(i => i.InviteId == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to get invitation by ID: {id}");
                return new Invites { InviteId = -1 };
            }
        }

        /// <summary>
        /// updates the invite details to the Db
        /// logs an error if there are issue
        /// </summary>
        /// <param name="item"></param>
        public void UpdateInvite(Invites item)
        {
            try
            {
                Invites.Update(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update invitation");
            }
        }

        /// <summary>
        /// Retrives an invite by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns> an invites by Email Address</returns>
        public Invites? GetInviteByEmail(string email)
        {
            return Invites.FirstOrDefault(i => i.EmailAddress == email);
        }

        /// <summary>
        /// Returns the InviteToLunchGroup object by email and lunchgroup
        /// </summary>
        /// <param name="email"></param>
        /// <param name="lunchgroup"></param>
        public InviteToLunchGroup? GetInviteByEmailAndLunchGroup(string email, string lunchgroup)
        {
            return InvitesLG.FirstOrDefault(invite => invite.UsernameEmail == email && invite.LunchGroupName == lunchgroup);
        }
    }
}
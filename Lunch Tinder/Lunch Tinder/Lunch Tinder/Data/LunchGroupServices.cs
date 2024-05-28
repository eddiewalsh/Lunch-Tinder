using Lunch_Tinder.Models;
using Lunch_Tinder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lunch_Tinder.Data
{

    public partial class LunchTinderContext
    {

        /// <summary>
        /// Adds the lunch group to the db and saves the change otherwsie it throws and exception
        /// </summary>
        /// <param name="item"></param>
        public void AddLunchGroup(LunchGroup item)
        {
            try
            {
                LunchGroups.Add(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add the lunch group");
            }
        }

        /// <summary>
        /// Deletes the lunch group from the database and saves that change, throws an exception if it fails
        /// </summary>
        /// <param name="item"></param>
        public void DeleteLunchGroup(LunchGroup item)
        {
            try
            {
                LunchGroups.Remove(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete the lunch group");
            }
        }

        /// <summary>
        ///  /// Gets all the lunch group content from the database, throws an exception if it fails
        /// </summary>
        /// <returns>logs error and returns a default vaule</returns>
        public List<LunchGroup> GetAllLunchGroups()
        {
            try
            {
                return LunchGroups
                       .Include(group => group.Users)
                       .Include(group => group.Events)
                       .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve all lunch groups");
                return new List<LunchGroup>();
            }
        }

        /// <summary>
        /// Gets all the lunch group content by ID from the database, throws an exception if it fails
        /// </summary>
        /// <param name="id"></param>
        /// <returns>logs error and returns a default vaule</returns>
        public LunchGroup? GetLunchGroupById(int id)
        {
            try
            {
                return LunchGroups.Include(group => group.Users)
                                  .Include(group => group.Events)
                                  .FirstOrDefault(group => group.GroupId == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve the lunch group by the set parameter");

                return new LunchGroup();
            }
        }

        /// <summary>
        ///  Updates the lunch group content from the database and saves that change, throws an exception if it fails
        /// </summary>
        /// <param name="item"></param>
        public void UpdateLunchGroup(LunchGroup item)
        {
            try
            {
                LunchGroups.Update(item);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update the lunch group");
            }
        }

        /// <summary>
        /// Gets lunch group by Lunch group Name, including its associated events and users
        /// </summary>
        /// <param name="name"></param>
        public LunchGroup? GetLunchGroupByName(string name)
        {
            return LunchGroups.Include(group => group.Users)
                              .Include(group => group.Events)
                              .FirstOrDefault(group => group.GroupName == name);
        }

        /// <summary>
        /// /// Retrieves the lunch group with the specified group ID from the database, including its associated events and users.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public LunchGroup? GetLunchGroup(int groupId)
        {
            return LunchGroups
                .Include(g => g.Events)
                .Include(lg => lg.Users)
                .FirstOrDefault(g => g.GroupId == groupId);
        }

        /// <summary>
        /// adds the invite to the DB or loggs an error if there are any exceptions
        /// </summary>
        /// <param name="invite"></param>
        public void AddInviteToLunchGroup(InviteToLunchGroup invite)
        {
            try
            {
                InvitesLG.Add(invite);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add the invite to the lunch group");
            }
        }

        /// <summary>
        /// updates the Group description for each individual group selected
        /// </summary>
        /// <param name="model"></param>
        public void UpdateGroupDescription(VMLunchEventForGroupModel model)
        {
            try
            {
                LunchGroups.Update(model.Group);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update Group Description");
            }
        }

        /// <summary>
        /// Retrieves the email address associated with the given username.
        /// </summary>
        /// <param name="username">The username to retrieve the email address for.</param>
        public string GetEmailByUsername(string username)
        {
            var user = Users.FirstOrDefault(u => u.UserName == username);
            return user?.EmailAddress;
        }

        /// <summary>
        /// Verifies if a user is a member of a lunchgroup
        /// </summary>
        /// <param name="lunchgroup"></param>
        /// <param name="user"></param>
        public bool VerifyUserIsLunchGroupMember(LunchGroup lunchgroup, User user)
        {
            return lunchgroup.Users.Any(lguser => lguser.UserId == user.UserId);
        }

        public void AddUserToLunchGroup(User user,LunchGroup lunchgroup)
        {
            LunchGroup lunchGroup = GetLunchGroupById(lunchgroup.GroupId);
            lunchGroup.Users.Add(user);
            LunchGroups.Update(lunchGroup);
            SaveChanges();
        }

        /// <summary>
        ///  returns a 
        /// </summary>
        /// <param name="user"></param>
        public List<LunchGroup> GetLunchGroupsByUser(User user)
        {
            return LunchGroups.Include(e => e.Events).Where(u => u.Users.Contains(user)).ToList();
        }

    }
}

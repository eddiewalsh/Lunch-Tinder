using Lunch_Tinder.Models;
using Lunch_Tinder.Security;
using Lunch_Tinder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lunch_Tinder.Data
{
    public partial class LunchTinderContext
    {

        /// <summary>
        /// Adds a User to the DB,
        /// logs an error if there is an exception.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            try
            {
                Users.Add(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Add a User to the Db");
            }

        }

        /// <summary>
        /// Deletes the User from the DB
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            try
            {
                Users.Remove(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Delete a User from the Db");
            }

        }

        /// <summary>
        /// Gets all users from the Db and put them into a list
        /// </summary>
        /// <returns>List of users</returns>
        public List<User> GetAllUsers()
        {
            try
            {
                return Users.Include(user => user.LunchGroups).ToList();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Could not retrieve users");
                return new List<User>();
            }

        }

        /// <summary>
        /// Gets thhe User by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User GetUserById(int id)
        {
            try
            {
                return Users.Include(user => user.LunchGroups)
                        .FirstOrDefault(i => i.UserId == id) ?? new User();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unable to retrieve user by ID");
                return new User();
            }
        }

        /// <summary>
        /// updates the user details
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            try
            {
                Users.Update(user);
                SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "unable to update the user details");
            }
        }

        /// <summary>
        /// Verifies the user credentials.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        public bool VerifyLoginCredentials(string email, string password)
        {
            try
            {
                User? matchingUser = Users.FirstOrDefault(user => user.EmailAddress.Equals(email));
                return PasswordHelper.VerifyPassword(password, matchingUser?.Password ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while verifying login credentials");
                return false;
            }

        }

        /// <summary>
        /// Gets the user by email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        public User? GetUserByEmail(string email)
        {
            try
            {
                User? verifieduser = Users.FirstOrDefault(user => user.EmailAddress.Equals(email));

                return verifieduser;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while retrieving user by email.");
                return new User();
            }

        }

        /// <summary>
        /// Verifies if the username is unique.
        /// </summary>
        /// <param name="username">The username to verify.</param>
        /// <returns>True if the username is unique; otherwise, false.</returns>
        public bool VerifyUserNameIsUnique(string username)
        {
            try
            {
                return !Users.Any(user => user.UserName != null && user.UserName.Trim().ToUpper().Equals(username.ToUpper()));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while verifying username uniqueness.");
                return false;
            }
        }

        /// <summary>
        /// Checks to see if an email is already in use.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is already in use; otherwise, false.</returns>
        public bool EmailAlreadyInUse(string email)
        {
            try
            {
                return Users.Any(user => user.EmailAddress == email);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Email alreadyInUse: {email}");
                return false;
            }

        }

        /// <summary>
        /// Retrieves a user with their associated lunch groups.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user with their lunch groups, or null if not found.</returns>
        public User? GetUserWithLunchGroups(int userId)
        {
            User? user = null;
            try
            {
                return Users
                    .Where(u => u.UserId == userId)
                    .Include(lg => lg.LunchGroups)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while retrieving user with lunch groups. User ID: {userId}");
            }
            return user;
        }

        /// <summary>
        /// Retrieves a lunch group with its related entities.
        /// </summary>
        /// <param name="groupId">The ID of the lunch group.</param>
        /// <returns>The lunch group with its related entities, or a default value if not found.</returns>
        public LunchGroup GetLunchGroupWithRelatedEntities(int groupId)
        {
            try
            {
                LunchGroup? lunchGroup = LunchGroups
                    .Include(g => g.Events)
                    .ThenInclude(e => e.RestaurantOptions)
                    .Include(g => g.Users)
                    .FirstOrDefault(g => g.GroupId == groupId);

                return lunchGroup ?? new LunchGroup(); // Return a default value if lunch group is null
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while retrieving lunch group with related entities. Group ID: {groupId}");
                return new LunchGroup(); // Return a default value in case of exception
            }
        }

        /// <summary>
        /// Retrieves a list of <see cref="InviteUserJoin"/> objects by performing a group join between the <see cref="Invites"/> and <see cref="Users"/> tables based on email address matching.
        /// </summary>
        /// <returns>A list of <see cref="InviteUserJoin"/> objects representing the joined data.</returns>
        public List<InviteUserJoin> GetInviteUserJoins()
        {
            var inviteUserJoins = Invites
                .GroupJoin(
                    Users,
                    invite => invite.EmailAddress,
                    user => user.EmailAddress,
                    (invite, users) => new InviteUserJoin
                    {
                        Invite = invite,
                        User = users.FirstOrDefault()
                    })
                .ToList();

            return inviteUserJoins;
        }
    }
}

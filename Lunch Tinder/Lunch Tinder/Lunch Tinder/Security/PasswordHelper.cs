namespace Lunch_Tinder.Security
{
    public class PasswordHelper
    {
        /// <summary>
        /// Encrypts the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Returns a hashed password</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies whether the provided password matches the hashed password.
        /// </summary>
        /// <param name="password">The password to be verified.</param>
        /// <param name="hashedPassword">The hashed password to compare against.</param>
        /// <returns>True if the password matches the hashed password, otherwise false.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

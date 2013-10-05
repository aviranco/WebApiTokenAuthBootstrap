using System;

namespace TokenAuthorization.Core.Account
{
    /// <summary>
    /// The user metadata avaiable, fetched automaticly by using the fetchers in TokenAuthConfiguration.Fetchers. 
    /// Cacheable by the 
    /// </summary>
    public class UserMetadata
    {
        public UserMetadata(DateTime lastAccess, int userId, string username, bool isAuthenticated, UserRole role)
        {
            Role = role;
            IsAuthenticated = isAuthenticated;
            Username = username;
            UserId = userId;
            LastAccess = lastAccess;
        }

        /// <summary>
        /// User Identity (Unique)
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// The Username of the user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Is the user authenticated or not.
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// The user's role.
        /// </summary>
        public UserRole Role { get; private set; }

        /// <summary>
        /// The date of the user's last access.
        /// </summary>
        public DateTime LastAccess { get; private set; }
    }
}
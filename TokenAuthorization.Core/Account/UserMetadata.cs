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

        public int UserId { get; private set; }

        public string Username { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public UserRole Role { get; private set; }

        public DateTime LastAccess { get; private set; }
    }
}
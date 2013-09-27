using System;
using TokenAuthorization.Core.Account;

namespace TokenAuthorization.Core
{
    public class TokenMetadata
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public UserRole Role { get; set; }

        public DateTime LastAccess { get; set; }
    }
}
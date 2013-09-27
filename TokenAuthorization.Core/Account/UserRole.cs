using System;

namespace TokenAuthorization.Core.Account
{
    /// <summary>
    /// Represents the role of the user, used to manage access according to its privileges.
    /// </summary>
    [Flags]
    public enum UserRole : uint
    {
        // User roles
        Unknown = 0,
        Public = 1,
        User = 2,
        Admin = 4
    }
}
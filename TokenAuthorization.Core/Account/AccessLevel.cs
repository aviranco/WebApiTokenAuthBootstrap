using System;

namespace TokenAuthorization.Core.Account
{
    /// <summary>
    /// Defines the access level of an http resource, used by the TokenAuthenticationAttribute.
    /// </summary>
    [Flags]
    public enum AccessLevel : uint
    {
        /// <summary>
        /// Access available to everyone.
        /// </summary>
        Public = UserRole.Public | UserRole.User | UserRole.Admin,

        /// <summary>
        /// Access only to unauthenticated users.
        /// </summary>
        Anonymous = UserRole.Public,

        /// <summary>
        /// Access to registered users (including admin).
        /// </summary>
        User = UserRole.User | UserRole.Admin,

        /// <summary>
        /// Access to admin.
        /// </summary>
        Admin = UserRole.Admin
    }
}
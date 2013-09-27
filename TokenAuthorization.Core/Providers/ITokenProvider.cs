using TokenAuthorization.Core.Account;

namespace TokenAuthorization.Core.Providers
{
    public interface ITokenProvider
    {
        bool IsTokenAuthorized(string token, AccessLevel accessLevel);

        string CreateToken(int userId, string username, UserRole role);

        bool DeleteToken(string token);

        TokenMetadata GetMetadata(string token);
    }
}
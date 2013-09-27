namespace TokenAuthorization.Core.TokenGenerators
{
    public interface ITokenGenerator
    {
        string GenerateToken();
    }
}
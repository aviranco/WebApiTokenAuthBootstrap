namespace TokenAuthorization.Core.Storage
{
    public interface ITokenStorage
    {
        TokenMetadata GetMetadata(string token);

        bool Add(string token, TokenMetadata tokenMetadata);

        bool Delete(string token);
    }
}
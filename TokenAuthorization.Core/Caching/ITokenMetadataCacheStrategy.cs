namespace TokenAuthorization.Core.Caching
{
    public interface ITokenMetadataCacheStrategy
    {
        void Cache(TokenMetadata metadata);

        bool IsTokenMetadataCached();

        TokenMetadata GetTokenMetadata();
    }
}
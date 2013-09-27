namespace TokenAuthorization.Core.Caching
{
    public class TokenCacheStrategy : IDataCacheStrategy, ITokenMetadataCacheStrategy
    {
        public void Cache(string data)
        {
            RouteCache.Token = data;
        }

        public void Cache(TokenMetadata metadata)
        {
            RouteCache.Metadata = metadata;
        }

        public bool IsDataCached()
        {
            return RouteCache.IsTokenCached;
        }

        public string GetDataCached()
        {
            return RouteCache.Token;
        }

        public bool IsTokenMetadataCached()
        {
            return RouteCache.IsTokenMetadataCached;
        }

        public TokenMetadata GetTokenMetadata()
        {
            return RouteCache.Metadata;
        }
    }
}
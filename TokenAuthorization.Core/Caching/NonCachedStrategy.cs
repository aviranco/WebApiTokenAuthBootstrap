namespace TokenAuthorization.Core.Caching
{
    public class NonCachedStrategy : IDataCacheStrategy, ITokenMetadataCacheStrategy
    {
        public void Cache(string data)
        {

        }

        public bool IsDataCached()
        {
            return false;
        }

        public string GetDataCached()
        {
            return null;
        }

        public void Cache(TokenMetadata metadata)
        {

        }

        public bool IsTokenMetadataCached()
        {
            return false;
        }

        public TokenMetadata GetTokenMetadata()
        {
            return null;
        }
    }
}
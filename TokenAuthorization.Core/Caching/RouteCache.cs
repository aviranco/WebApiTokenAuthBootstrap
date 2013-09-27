namespace TokenAuthorization.Core.Caching
{
    public static class RouteCache
    {
        private static string _token;
        public static string Token
        {
            get
            {

                return _token;
            }
            set
            {
                IsTokenCached = true;
                _token = value;
            }
        }

        private static TokenMetadata _metadata;
        public static TokenMetadata Metadata
        {
            get { return _metadata; }
            set
            {
                IsTokenMetadataCached = true;
                _metadata = value;
            }
        }

        public static bool IsTokenMetadataCached { get; private set; }

        public static bool IsTokenCached { get; private set; }

    }
}
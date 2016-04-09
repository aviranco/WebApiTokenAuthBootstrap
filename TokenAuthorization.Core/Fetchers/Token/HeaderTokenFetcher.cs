using System;
using TokenAuthorization.Core.Caching;
using TokenAuthorization.Core.Configuration;
using TokenAuthorization.Core.Fetchers.Data;

namespace TokenAuthorization.Core.Fetchers.Token
{
    public class HeaderTokenFetcher : HeaderDataFetcher, ITokenFetcher
    {
        public HeaderTokenFetcher()
            : base(new Lazy<string>(() => TokenAuthenticationConfiguration.TokenName), new TokenCacheStrategy())
        {
        }
    }
}
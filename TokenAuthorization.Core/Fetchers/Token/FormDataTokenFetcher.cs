using System;
using TokenAuthorization.Core.Caching;
using TokenAuthorization.Core.Configuration;
using TokenAuthorization.Core.Fetchers.Data;

namespace TokenAuthorization.Core.Fetchers.Token
{
    public class FormDataTokenFetcher : FormDataFetcher, ITokenFetcher
    {
        public FormDataTokenFetcher()
            : base(new Lazy<string>(() => TokenAuthenticationConfiguration.TokenName), new TokenCacheStrategy())
        {
        }
    }
}
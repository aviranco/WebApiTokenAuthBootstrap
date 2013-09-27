using System;
using System.Linq;
using System.Net.Http;
using TokenAuthorization.Core.Caching;

namespace TokenAuthorization.Core.Fetchers.Data
{
    public class CookieDataFetcher : CacheableDataFetcher
    {
public CookieDataFetcher(string dataName) : base(dataName, new NonCachedStrategy())
        {
        }

        public CookieDataFetcher(Lazy<string> lazyDataName) : base(lazyDataName, new NonCachedStrategy())
        {
        }

        public CookieDataFetcher(string dataName, IDataCacheStrategy cacheStrategy)
            : base(dataName, cacheStrategy)
        {
        }

        public CookieDataFetcher(Lazy<string> lazyDataName, IDataCacheStrategy cacheStrategy)
            : base(lazyDataName, cacheStrategy)
        {
        }

        public override string CacheableFetchData(HttpRequestMessage request)
        {
            var cookies = request.Headers.GetCookies(DataName);

            if (cookies == null || cookies.Count != 1)
            {
                return null;
            }

            var cookie = cookies[0].Cookies.FirstOrDefault(c => c.Name == DataName);

            return cookie == null ? null : cookie.Value;

        }
    }
}
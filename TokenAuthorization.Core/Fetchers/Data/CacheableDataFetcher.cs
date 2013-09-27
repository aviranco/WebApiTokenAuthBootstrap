using System;
using System.Net.Http;
using TokenAuthorization.Core.Caching;
using TokenAuthorization.Core.Fetchers.Token;

namespace TokenAuthorization.Core.Fetchers.Data
{
    public abstract class CacheableDataFetcher : BaseDataFetcher, ITokenFetcher, ICacheable
    {
        private IDataCacheStrategy _cacheStrategy;

        protected CacheableDataFetcher(string dataName, IDataCacheStrategy cacheStrategy)
            : base(dataName)
        {
            _cacheStrategy = cacheStrategy;
        }

        protected CacheableDataFetcher(Lazy<string> lazyDataName, IDataCacheStrategy cacheStrategy)
            : base(lazyDataName)
        {
            _cacheStrategy = cacheStrategy;
        }


        public sealed override string FetchData(HttpRequestMessage request)
        {
            var data = CacheableFetchData(request);

            _cacheStrategy.Cache(data);

            return data;
        }

        public abstract string CacheableFetchData(HttpRequestMessage request);
    }
}
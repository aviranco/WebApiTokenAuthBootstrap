using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using TokenAuthorization.Core.Caching;

namespace TokenAuthorization.Core.Fetchers.Data
{
    /// <summary>
    /// If the Token parameter exists in the Web API action parameters it won't work,
    /// since if Web API is successfully binding Token to a model the Request.Content will not return anything.
    /// Therefore, the HasData() method will always return false.
    /// </summary>
    public class FormDataFetcher : CacheableDataFetcher
    {
        public FormDataFetcher(string dataName) : base(dataName, new NonCachedStrategy())
        {
        }

        public FormDataFetcher(Lazy<string> lazyDataName) : base(lazyDataName, new NonCachedStrategy())
        {
        }

        public FormDataFetcher(string dataName, IDataCacheStrategy cacheStrategy)
            : base(dataName, cacheStrategy)
        {
        }

        public FormDataFetcher(Lazy<string> lazyDataName, IDataCacheStrategy cacheStrategy)
            : base(lazyDataName, cacheStrategy)
        {
        }

        public override string CacheableFetchData(HttpRequestMessage request)
        {
            string data = request.Content.ReadAsStringAsync().Result;
            var expandoData = JsonConvert.DeserializeObject<dynamic>(data);

            // TODO: check for exceptions
            if (((IDictionary<string, object>)expandoData).ContainsKey(DataName))
            {
                return ((IDictionary<string, object>) expandoData)[DataName].ToString();
            }

            return null;
        }
    }
}
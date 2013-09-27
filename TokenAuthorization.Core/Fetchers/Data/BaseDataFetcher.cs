using System;
using System.Net.Http;

namespace TokenAuthorization.Core.Fetchers.Data
{
    public abstract class BaseDataFetcher : IDataFetcher
    {
        private Lazy<string> _lazyDataName;
        private string _dataName;

        protected string DataName
        {
            get
            {
                if (_lazyDataName != null)
                {
                    return _lazyDataName.Value;
                }

                return _dataName;
            }
        }

        protected BaseDataFetcher(string dataName)
        {
            if (string.IsNullOrEmpty(dataName))
            {
                throw new ArgumentException("dataName cannot be null or empty", "dataName");
            }
            _dataName = dataName;
        }

        protected BaseDataFetcher(Lazy<string> lazyDataName)
        {
            if (lazyDataName == null)
            {
                throw new ArgumentException("lazyDataName cannot be null", "lazyDataName");
            }

            _lazyDataName = lazyDataName;
        }

        public bool HasData(HttpRequestMessage request)
        {
            var data = FetchData(request);

            return data != null;
        }

        public abstract string FetchData(HttpRequestMessage request);
    }
}
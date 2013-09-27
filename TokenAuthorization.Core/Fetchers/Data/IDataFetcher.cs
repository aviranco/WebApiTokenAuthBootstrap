using System.Net.Http;

namespace TokenAuthorization.Core.Fetchers.Data
{
    public interface IDataFetcher
    {
        bool HasData(HttpRequestMessage request);

        string FetchData(HttpRequestMessage request);
    }
}
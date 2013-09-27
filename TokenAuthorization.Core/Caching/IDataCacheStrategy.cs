namespace TokenAuthorization.Core.Caching
{
    public interface IDataCacheStrategy
    {
        void Cache(string data);

        bool IsDataCached();

        string GetDataCached();
    }
}
using System;
using System.Collections.Concurrent;

namespace TokenAuthorization.Core.Storage
{
    public class InMemoryTokenStorage : ITokenStorage
    {
        private static ConcurrentDictionary<string, TokenMetadata> _dictionary = new ConcurrentDictionary<string, TokenMetadata>();

        public TokenMetadata GetMetadata(string token)
        {
            throw new NotImplementedException();
        }

        public bool Add(string token, TokenMetadata tokenMetadata)
        {
            return _dictionary.TryAdd(token, tokenMetadata);
        }

        public bool Delete(string token)
        {
            TokenMetadata metadata;
            return _dictionary.TryRemove(token, out metadata);
        }
    }
}
using System;
using System.Collections.Concurrent;

namespace TokenAuthorization.Core.Storage
{
    public class InMemoryTokenStorage : ITokenStorage
    {
        private static ConcurrentDictionary<string, TokenMetadata> _dictionary = new ConcurrentDictionary<string, TokenMetadata>();

        public TokenMetadata GetMetadata(string token)
        {
            TokenMetadata metadata;
            bool success = _dictionary.TryGetValue(token, out metadata);

            return success ? metadata : null;
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

        public void UpdateLastAccess(string token, DateTime accessDate)
        {
            TokenMetadata metadata;
            bool success = _dictionary.TryGetValue(token, out metadata);

            if (success)
            {
                metadata.LastAccess = DateTime.Now;
            }

            // TODO: review if necessary and done well.
            _dictionary.TryUpdate(token, metadata, metadata);
        }
    }
}
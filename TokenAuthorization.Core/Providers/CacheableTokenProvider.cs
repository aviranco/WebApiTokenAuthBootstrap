using System;
using TokenAuthorization.Core.Account;
using TokenAuthorization.Core.Caching;
using TokenAuthorization.Core.Configuration;
using TokenAuthorization.Core.Storage;
using TokenAuthorization.Core.TokenGenerators;

namespace TokenAuthorization.Core.Providers
{
    public class CacheableTokenProvider : ITokenProvider
    {
        private InMemoryTokenStorage _cache;
        private ITokenStorage _storage;
        private ITokenGenerator _generator;
        private ITokenMetadataCacheStrategy _tokenMetadataCacheStrategy;

        public CacheableTokenProvider()
            : this(TokenAuthenticationConfiguration.TokenStorage, new Base64TokenGenerator())
        {
            
        }

        public CacheableTokenProvider(ITokenStorage tokenStorage)
            : this(tokenStorage, new Base64TokenGenerator())
        {

        }

        public CacheableTokenProvider(ITokenStorage tokenStorage, ITokenGenerator tokenGenerator) :
            this(tokenStorage, tokenGenerator, new TokenCacheStrategy())
        {

        }

        public CacheableTokenProvider(ITokenStorage tokenStorage, ITokenGenerator tokenGenerator, ITokenMetadataCacheStrategy tokenMetadataCacheStrategy)
        {
            _tokenMetadataCacheStrategy = tokenMetadataCacheStrategy;
            _cache = new InMemoryTokenStorage();
            _generator = tokenGenerator;
            _storage = tokenStorage;
        }

        public bool IsTokenAuthorized(string token, AccessLevel accessLevelRequired)
        {
            if (accessLevelRequired == AccessLevel.Public)
            {
                return true;
            }

            var metadata = _storage.GetMetadata(token);

            if (metadata == null)
            {
                return false;
            }

            switch (accessLevelRequired)
            {
                case AccessLevel.Admin:
                    return metadata.Role == UserRole.Admin;

                case AccessLevel.User:
                    return metadata.Role == UserRole.Admin || metadata.Role == UserRole.User;

                case AccessLevel.Public:
                    return true;

                case AccessLevel.Anonymous:
                    return metadata.Role == UserRole.Public;
            }

            return true;
        }

        public string CreateToken(int userId, string username, UserRole role)
        {
            var newToken = _generator.GenerateToken();
            var metadata = new TokenMetadata()
                {
                    LastAccess = DateTime.Now,
                    UserId = userId,
                    Username = username,
                    Role = role
                };

            _storage.Add(newToken, metadata);
            _cache.Add(newToken, metadata);

            return newToken;
        }

        public bool DeleteToken(string token)
        {
            return _storage.Delete(token) && _cache.Delete(token);
        }

        public TokenMetadata GetMetadata(string token)
        {
            var metadata = _cache.GetMetadata(token);

            if (metadata != null)
            {
                metadata.LastAccess = DateTime.Now;
                _tokenMetadataCacheStrategy.Cache(metadata);

                return metadata;
            }

            metadata = _storage.GetMetadata(token);

            if (metadata != null)
            {
                metadata.LastAccess = DateTime.Now;
                _cache.Add(token, metadata);
                _tokenMetadataCacheStrategy.Cache(metadata);

                return metadata;
            }

            return null;
        }
    }
}
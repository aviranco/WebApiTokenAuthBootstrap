using System;
using System.Collections.Generic;
using TokenAuthorization.Core.Fetchers.Data;
using TokenAuthorization.Core.Fetchers.Token;
using TokenAuthorization.Core.Providers;
using TokenAuthorization.Core.Storage;
using TokenAuthorization.Core.TokenGenerators;

namespace TokenAuthorization.Core.Configuration
{
    public static class TokenAuthenticationConfiguration
    {
        public static List<ITokenFetcher> TokenFetchers { get; set; }
        public static ITokenProvider TokenProvider { get; set; }
        public static IDataFetcher UserFetcher { get; set; }
        public static ITokenStorage TokenStorage { get; set; }
        public static Dictionary<string, string> UnauthorizedMessageResponse { get; set; }
        public static string TokenName { get; set; }
        public static string UserCookieName { get; set; }
        public static object RolePropertyName { get; set; }
        public static bool ValidateInitialization { get { return true; } }

        private const string DefaultRolePropertyName = "role";
        private const string DefaultUnauthorizedMessageName = "You are no authorized to access this resource.";
        private const string DefaultUnauthorizedMessage = "You are no authorized to access this resource.";
        private const string DefaultTokenName = "token";
        private const string DefaultUserCookieName = "user";

        static TokenAuthenticationConfiguration()
        {
            TokenStorage = new InMemoryTokenStorage();
            UserFetcher = new CookieDataFetcher(new Lazy<string>(() => UserCookieName));
            RolePropertyName = DefaultRolePropertyName;
            TokenName = DefaultTokenName;
            UserCookieName = DefaultUserCookieName;
            TokenFetchers = new List<ITokenFetcher> { new CookieTokenFetcher() };
            TokenProvider = new CacheableTokenProvider(new InMemoryTokenStorage(), new Base64TokenGenerator());
            UnauthorizedMessageResponse = new Dictionary<string, string>
                {
                    {DefaultUnauthorizedMessageName, DefaultUnauthorizedMessage}
                };
        }

    }
}
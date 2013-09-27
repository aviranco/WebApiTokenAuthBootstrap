using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TokenAuthorization.Core.Account;
using TokenAuthorization.Core.Configuration;
using TokenAuthorization.Core.Fetchers.Token;
using TokenAuthorization.Core.Providers;

namespace TokenAuthorization.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class TokenAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private AccessLevel _accessLevel;

        private ITokenProvider _tokenProvider;

        private List<ITokenFetcher> _tokenFetchers;

        public TokenAuthenticationAttribute(AccessLevel accessLevel, ITokenProvider tokenProvider, List<ITokenFetcher> tokenFetchers)
        {
            if (tokenProvider == null)
            {
                throw new ArgumentNullException("tokenProvider");
            }

            if (tokenFetchers == null)
            {
                throw new ArgumentNullException("tokenFetchers");
            }

            if (!tokenFetchers.Any())
            {
                throw new ArgumentException("tokenFetchers cann't be empty", "tokenFetchers");
            }

            _accessLevel = accessLevel;
            _tokenProvider = tokenProvider;
            _tokenFetchers = tokenFetchers;
        }

        public TokenAuthenticationAttribute(AccessLevel accessLevel)
            : this(accessLevel, TokenAuthenticationConfiguration.TokenProvider, TokenAuthenticationConfiguration.TokenFetchers)
        {
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            if (!IsAuthorized(context))
            {

                context.Response =
                    context.Request.CreateResponse(
                        HttpStatusCode.Unauthorized,
                        TokenAuthenticationConfiguration.UnauthorizedMessageResponse
                        );

            }
        }

        protected bool IsAuthorized(HttpActionContext context)
        {
            if (_accessLevel == AccessLevel.Public)
            {
                return true;
            }

            string token = null;

            foreach (var tokenFetcher in _tokenFetchers)
            {
                if (tokenFetcher.HasData(context.Request))
                {
                    token = tokenFetcher.FetchData(context.Request);
                    break;
                }
            }

            if (token == null)
            {
                return false;
            }

            return IsTokenAuthorized(token, _accessLevel);

        }

        private bool IsTokenAuthorized(string token, AccessLevel accessLevel)
        {
            return _tokenProvider.IsTokenAuthorized(token, accessLevel);
        }
    }
}
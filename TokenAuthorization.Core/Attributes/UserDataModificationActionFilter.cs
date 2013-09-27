using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using TokenAuthorization.Core.Account;
using TokenAuthorization.Core.Caching;
using TokenAuthorization.Core.Configuration;
using TokenAuthorization.Core.Controllers;

namespace TokenAuthorization.Core.Attributes
{
    public class UserDataModificationActionFilter : ActionFilterAttribute
    {
        private const string LocalHost = "localhost";

        private TokenCacheStrategy _cacheStrategy = new TokenCacheStrategy();

        public override void OnActionExecuting(HttpActionContext context)
        {
            var controller = context.ControllerContext.Controller as TokenAuthApiController;

            if (controller == null)
            {
                return;
            }

            if (_cacheStrategy.IsTokenMetadataCached())
            {
                var metadata = _cacheStrategy.GetTokenMetadata();

                if (metadata != null)
                {
                    controller.User = new UserMetadata(metadata.LastAccess, metadata.UserId, metadata.Username, true,
                                                   metadata.Role);
                }
            }
            else if (_cacheStrategy.IsDataCached())
            {

                var token = _cacheStrategy.GetDataCached();
                if (token != null)
                {
                    var metadata = TokenAuthenticationConfiguration.TokenStorage.GetMetadata(token);

                    if (metadata != null)
                    {
                        controller.User = new UserMetadata(metadata.LastAccess, metadata.UserId, metadata.Username, true,
                                       metadata.Role);
                    }
                }

            }
            else
            {
                var tokenFetchers = TokenAuthenticationConfiguration.TokenFetchers;
                string token = null;

                foreach (var tokenFetcher in tokenFetchers)
                {
                    if (tokenFetcher.HasData(context.Request))
                    {
                        token = tokenFetcher.FetchData(context.Request);
                        break;
                    }
                }

                if (token != null)
                {
                    var metadata = TokenAuthenticationConfiguration.TokenStorage.GetMetadata(token);

                    if (metadata != null)
                    {
                        controller.User = new UserMetadata(metadata.LastAccess, metadata.UserId, metadata.Username, true,
                                       metadata.Role);
                    }
                }
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var controller = context.ActionContext.ControllerContext.Controller as TokenAuthApiController;

            if (controller == null || !controller.PossibleDataModified)
            {
                return;
            }

            var newData = controller.UserData;

            var stringData = JsonConvert.SerializeObject(newData ?? string.Empty);

            var userCookie = new CookieHeaderValue(TokenAuthenticationConfiguration.UserCookieName, stringData)
                {
                    Expires = DateTimeOffset.MaxValue,
                    Domain = context.Request.RequestUri.Host == LocalHost ? null : context.Request.RequestUri.Host,
                    Path = "/"
                };

            context.Response.Headers.AddCookies(new[] { userCookie });
        }
    }
}

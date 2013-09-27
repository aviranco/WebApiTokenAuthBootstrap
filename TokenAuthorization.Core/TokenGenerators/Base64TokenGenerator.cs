using System;

namespace TokenAuthorization.Core.TokenGenerators
{
    /// <summary>
    /// Base64 Token Generator, excluding the '=' and '+' signs.
    /// </summary>
    public class Base64TokenGenerator : ITokenGenerator
    {
        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string token = Convert.ToBase64String(g.ToByteArray());
            return token.Replace("=", string.Empty).Replace("+", string.Empty);
        }
    }
}
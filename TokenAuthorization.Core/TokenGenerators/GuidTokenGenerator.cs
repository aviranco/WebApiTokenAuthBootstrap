using System;

namespace TokenAuthorization.Core.TokenGenerators
{
    /// <summary>
    /// Guid Token generator.
    /// </summary>
    public class GuidTokenGenerator : ITokenGenerator
    {
        public string GenerateToken()
        {
            return Guid.NewGuid().ToString();
            
        }
    }
}
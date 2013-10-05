using System;

namespace TokenAuthorization.Core.Storage
{
    /// <summary>
    /// Provides storage implementation for the auth tokens.
    /// </summary>
    public interface ITokenStorage
    {
        /// <summary>
        /// Retrieves metadata for the given token. If token not exist, return null.
        /// </summary>
        /// <param name="token">The token used to get the matching the metadata for</param>
        /// <returns>If token not exist, return null.</returns>
        TokenMetadata GetMetadata(string token);

        /// <summary>
        /// Adds the given token to the storage along with its matching metadatadata.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenMetadata"></param>
        /// <returns>Whether added successfully or not.</returns>
        bool Add(string token, TokenMetadata tokenMetadata);

        /// <summary>
        /// Deletes the token from the storage along with its matching metadata.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Whether deleted successfully or not.</returns>
        bool Delete(string token);

        /// <summary>
        /// Optional - Update the last access date on user access. You can skip its implementation
        /// in order to improve performance (Chached requests won't update the last access date).
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accessDate"></param>
        /// <returns>Whether updated successfully or not.</returns>
        void UpdateLastAccess(string token, DateTime accessDate);
    }
}
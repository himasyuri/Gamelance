using GamelanceAuth.Models;
using GamelanceAuth.Models.Responses;

namespace GamelanceAuth.Services
{
    public interface IJWTAuth
    {
        Task<TokensResponse> GenerateTokens(User user, string userAgent);

        Task<TokensResponse> UpdateTokens(string token, string userAgent);

        void RevokeToken(string token);
    }
}

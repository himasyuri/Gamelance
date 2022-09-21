using GamelanceAuth.Models;
using GamelanceAuth.Models.Responses;

namespace GamelanceAuth.Services
{
    public interface IJWTAuth
    {
        Task<TokensResponse> GenerateTokens(User user, string device, string location);

        Task<TokensResponse> UpdateTokens(string token, string device, string location);

        void RevokeToken(string token);
    }
}

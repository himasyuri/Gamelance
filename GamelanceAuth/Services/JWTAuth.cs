using AuthCommon;
using GamelanceAuth.Data;
using GamelanceAuth.Models;
using GamelanceAuth.Models.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GamelanceAuth.Services
{
    public class JWTAuth : IJWTAuth
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IRoleManager _roleManager;
        private readonly DataContext _context;

        public JWTAuth(IOptions<AuthOptions> authOptions, IRoleManager roleManager, DataContext context)
        {
            _authOptions = authOptions;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<TokensResponse> GenerateTokens(User user, string userAgent)
        {
            User? checkUser = await _context.Users.FindAsync(user.Id);

            if (checkUser.RefreshTokens.Count > 5)
            {
                RemoveOldRefreshTokens(checkUser);
            }

            var refreshToken = CreateRefreshToken(userAgent);
            var accessToken = await CreateAccessToken(user);

            refreshToken.User = user;
            _context.RefreshTokens.Add(refreshToken);

            TokensResponse response = new TokensResponse
            {
                RefreshToken = refreshToken.Token,
                AccessToken = accessToken,
            };

            _context.Users.Update(checkUser);
            _context.SaveChanges();

            return response;
        }

        public async Task<TokensResponse> UpdateTokens(string token, string userAgent)
        {

            User user = await GetUserByRefreshToken(token);
            var refreshToken = _context.RefreshTokens.SingleOrDefault(t => t.Token == token);

            if (!VerifyHash(userAgent, refreshToken.UserAgentHash))
            {
                throw new Exception("Invalid token");
            }

            if (!refreshToken.IsActive)
            {
                throw new Exception("Invalid token");
            }

            var newRefreshToken = RotateRefreshToken(userAgent);

            newRefreshToken.User = user;
            _context.RefreshTokens.Add(newRefreshToken);
            RemoveOldRefreshTokens(user);

            _context.Update(user);
            await _context.SaveChangesAsync();

            var accessToken = await CreateAccessToken(user);

            TokensResponse response = new TokensResponse
            {
                RefreshToken = newRefreshToken.Token,
                AccessToken = accessToken,
            };

            return response;
        }

        public void RevokeToken(string token)
        {
            User user = GetUserByRefreshToken(token).Result;
            var refreshToken = _context.RefreshTokens.SingleOrDefault(t => t.Token == token);

            _context.RefreshTokens.Remove(refreshToken);

            _context.Update(user);
            _context.SaveChanges();
        }

        private RefreshToken CreateRefreshToken(string userAgent)
        {
            var authParams = _authOptions.Value;

            RefreshToken resreshToken = new RefreshToken
            {
                Token = CreateUniqueToken(),
                UserAgentHash = Convert.ToString(GetHash(userAgent)),
                Exspires = DateTime.Now.AddDays(authParams.RefreshTokenLifeTime),
            };

            var tokenIsUnique = _context.Users.Any(u => u.RefreshTokens.Any(t => t.Token == resreshToken.Token));

            if (tokenIsUnique)
            {
                CreateRefreshToken(userAgent);
            }

            return resreshToken;
        }

        private async Task<string> CreateAccessToken(User user)
        {
            var authParams = _authOptions.Value;
            var sequrityKey = authParams.GetSymmetricSecurityKey();
            var creditionals = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };
            var userRoles = await _roleManager.GetUserRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("roles", role));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(authParams.AccessTokenLifeTime),
                signingCredentials: creditionals);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string CreateUniqueToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(65));
        }

        private async Task<User> GetUserByRefreshToken(string token)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(
                u => _context.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new Exception("Invalid Token");
            }

            return user;
        }

        private RefreshToken RotateRefreshToken(string userAgent)
        {
            var newRefreshToken = CreateRefreshToken(userAgent);

            return newRefreshToken;
        }

        private static void RemoveOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(p =>
            p.IsActive == false && p.IsExpired == true);
        }

        private static byte[] GetHash(string userAgent)
        {
            using (SHA256 alg = SHA256.Create())
            {
                var hash = alg.ComputeHash(Encoding.UTF8.GetBytes(userAgent));

                return hash;
            }
        }

        private static bool VerifyHash(string userAgent, string oldHash)
        {
            byte[] hash;

            using (SHA256 alg = SHA256.Create())
            {
                hash = alg.ComputeHash(Encoding.UTF8.GetBytes(userAgent));

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                return comparer.Compare(oldHash, Convert.ToString(hash)) == 0;
            }
        }
    }

}

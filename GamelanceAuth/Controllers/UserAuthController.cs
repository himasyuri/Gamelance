using GamelanceAuth.Models.Dto;
using GamelanceAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamelanceAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _authService;
        private readonly IJWTAuth _jwt;

        public UserAuthController(IUserAuthService authService, IJWTAuth jwt)
        {
            _authService = authService;
            _jwt = jwt;
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration(UserRegisterRequest request)
        {
            var result = await _authService.Register(request);
            var token = await _jwt.GenerateTokens(result, GetUserAgent());
            SetTokenCookie(token.RefreshToken);

            return Ok(token);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var result = await _authService.Login(request);
            var token = await _jwt.GenerateTokens(result, GetUserAgent());
            SetTokenCookie(token.RefreshToken);

            return Ok(token);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens()
        {
            var userAgent = HttpContext.Request.Headers.UserAgent.ToString();
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];

            if (refreshToken == null)
            {
                throw new Exception("Invalid token");
            }

            var token = await _jwt.UpdateTokens(refreshToken, userAgent);
            SetTokenCookie(token.RefreshToken);

            return Ok(token);
        }

        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];
            DeleteTokenCookie();
            _jwt.RevokeToken(refreshToken);

            return Ok();
        }

        [Route("Change-username")]
        [HttpPut]
        public async Task<IActionResult> ChangeUserName(ChangeUserName request,Guid userId)
        {
            var result = await _authService.ChangeUserName(request, userId);

            return Ok(result);
        }

        [Route("Forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _authService.ForgotPasswordAsync(email);

            return Ok();
        }

        [Route("Reset-password")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _authService.ResetPasswordAsync(request);

            return Ok();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(30)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private void DeleteTokenCookie()
        {
            Response.Cookies.Delete("refreshToken");
        }

        private string GetUserAgent()
        {
            return HttpContext.Request.Headers.UserAgent.ToString();
        }

    }
}

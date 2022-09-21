using GamelanceAuth.Models;
using GamelanceAuth.Models.Dto;

namespace GamelanceAuth.Services
{
    public interface IUserAuthService
    {
        ValueTask<User> Register(UserRegisterRequest request);

        ValueTask<User> ChangeUserName(ChangeUserName newName, Guid userId);

        ValueTask<User> Login(UserLoginRequest request);

        ValueTask Verify(string token);

        ValueTask ForgotPasswordAsync(string email);

        ValueTask ResetPasswordAsync(ResetPasswordRequest request);
    }
}

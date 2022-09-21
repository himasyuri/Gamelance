using GamelanceAuth.Data;
using GamelanceAuth.Models;
using GamelanceAuth.Models.Dto;
using System.Security.Cryptography;

namespace GamelanceAuth.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly DataContext _context;
        private readonly IRoleManager _roleManager;

        public UserAuthService(DataContext context, IRoleManager roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async ValueTask<User> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Password is incorrect");
            }

            return user;
        }

        public async ValueTask<User> ChangeUserName(ChangeUserName newName, Guid userId)
        {
            if (_context.Users.Any(u => u.UserName == newName.UserName))
            {
                throw new Exception("Already exists");
            }

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.UserName = newName.UserName;
            await _context.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email & u.UserName == request.UserName))
            {
                throw new Exception("User aready exists");
            }

            CreatePasswordHash(request.Password,
                              out byte[] passwordHash,
                              out byte[] passwordSalt);
            User user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                UserName = request.UserName,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            await _context.Users.AddAsync(user);
            await _roleManager.AddToRoleAsync(user, "user");
            await _context.SaveChangesAsync();

            return user;
        }

        public async ValueTask Verify(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {
                throw new Exception("Invalid token");
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return;
        }

        public async ValueTask ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("Invalid Email");
            }
            
            while (_context.Users.Any(p => p.PasswordResetToken == user.PasswordResetToken))
            {
                user.PasswordResetToken = CreateRandomToken();
            }

            user.ResetTokenExpires = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();

            return;
        }

        public async ValueTask ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);

            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                throw new Exception("Invalid token");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = default!;
            user.ResetTokenExpires = default!;

            await _context.SaveChangesAsync();

            return;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(65));
        }
    }
}

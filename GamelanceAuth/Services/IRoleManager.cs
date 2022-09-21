using GamelanceAuth.Models;

namespace GamelanceAuth.Services
{
    public interface IRoleManager
    {
        ValueTask<bool> CreateRoleAsync(Role role);

        ValueTask<Role> EditRoleAsync(Role role, string roleName);

        ValueTask DeleteRoleAsync(Role role);

        ValueTask<bool> AddToRoleAsync(User user, string roleName);

        ValueTask<bool> RemoveUserRoleAsync(User user, string roleName);

        ValueTask<List<string>> GetUserRolesAsync(User user);

        ValueTask<List<string>> GetRolesAsync();
    }
}

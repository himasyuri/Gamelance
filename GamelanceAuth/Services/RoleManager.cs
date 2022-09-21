using GamelanceAuth.Data;
using GamelanceAuth.Models;

namespace GamelanceAuth.Services
{
    public class RoleManager : IRoleManager
    {
        private readonly DataContext _context;

        public RoleManager(DataContext context)
        {
            _context = context;
        }

        public async ValueTask<bool> AddToRoleAsync(User user, string roleName)
        {
            Role newrole = new Role() { RoleName = roleName };
            await CreateRoleAsync(newrole);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            UserRole userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = role.RoleId
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return true;
        }

        public async ValueTask<bool> CreateRoleAsync(Role model)
        {
            if (model.RoleName == null)
            {
                throw new ArgumentNullException(nameof(model.RoleName));
            }

            Role role = new Role
            {
                RoleName = model.RoleName
            };

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();

            return true;
        }

        public async ValueTask DeleteRoleAsync(Role model)
        {
            var role = await _context.Roles.FindAsync(model);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return;
        }

        public async ValueTask<Role> EditRoleAsync(Role model, string roleName)
        {
            var role = await _context.Roles.FindAsync(model);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.RoleName = roleName;
            await _context.SaveChangesAsync();

            return role;
        }

        public async ValueTask<List<string>> GetRolesAsync()
        {
            return await _context.Roles.Select(x => x.RoleName).ToListAsync();
        }

        public async ValueTask<List<string>> GetUserRolesAsync(User user)
        {
            var query = from userRole in _context.UserRoles
                        where userRole.UserId.Equals(user.Id)
                        join role in _context.Roles on userRole.RoleId equals role.RoleId
                        select role.RoleName;

            return await query.ToListAsync();
        }

        public async ValueTask<bool> RemoveUserRoleAsync(User user, string roleName)
        {
            var role = await _context.Roles.FindAsync(roleName);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role doesnt exist");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User not found");
            }

            UserRole? userRole = await _context.UserRoles.FirstOrDefaultAsync(p => p.UserId.Equals(user.Id) && p.RoleId.Equals(role.RoleId));

            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole), "User doesnt have this role");
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

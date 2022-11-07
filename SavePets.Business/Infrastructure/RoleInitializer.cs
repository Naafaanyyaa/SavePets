using Microsoft.AspNetCore.Identity;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Infrastructure
{
    public class RoleInitializer : IRoleInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleInitializer(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeIdentityData()
        {
            RegisterRoleAsync(CustomRoles.AdminRole).Wait();
            RegisterRoleAsync(CustomRoles.UserRole).Wait();
        }

        private async Task<Role> RegisterRoleAsync(string roleName)
        {

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                return role;
            }

            role = new Role(roleName);
            await _roleManager.CreateAsync(role);

            return role;
        }

        private async Task InitializeSuperAdminRole()
        {

            var superAdmin = _userManager.Users.FirstOrDefault(u => u.UserName == "root") ?? RegisterSuperAdmin();

            var superAdminRole = await RegisterRoleAsync(CustomRoles.SuperAdminRole);

            if (!await _userManager.IsInRoleAsync(superAdmin, CustomRoles.SuperAdminRole))
                await _userManager.AddToRoleAsync(superAdmin, superAdminRole.Name);
        }

        private User RegisterSuperAdmin()
        {

            var superAdmin = new User()
            {
                FirstName = "Root",
                LastName = "Root",
                UserName = "root",
                Email = "root@save-pets.com"
            };

            _userManager.CreateAsync(superAdmin, "_QGrXyvcmTD4aVQJ_").Wait();

            return superAdmin;
        }
    }
}

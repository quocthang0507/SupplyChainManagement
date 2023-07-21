using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class Roles : IRoles
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Roles(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task GenerateAllExistingRolesAsync()
        {
            Type roleType = typeof(RoleNames);
            foreach (var itm in roleType.GetFields())
            {
                    string roleName = itm.GetValue(roleType).ToString();
                    if (!await _roleManager.RoleExistsAsync(roleName))
                        await _roleManager.CreateAsync(new ApplicationRole() { Name = roleName });
            }
        }

        public async Task AssignAllRolesToUserAsync(string applicationUserId)
        {
            var user = await _userManager.FindByIdAsync(applicationUserId);
            if (user != null)
            {
                var roles = _roleManager.Roles;
                List<string> listRoles = new();
                foreach (var item in roles)
                {
                    listRoles.Add(item.Name);
                }
                await _userManager.AddToRolesAsync(user, listRoles);
            }
        }
    }
}
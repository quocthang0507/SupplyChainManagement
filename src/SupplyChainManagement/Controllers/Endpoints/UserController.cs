using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SupplyChainManagement.Models;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserProfilesService _userProfilesService;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, UserProfilesService userProfilesService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userProfilesService = userProfilesService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            List<UserProfile> Items = new();
            Items = await _userProfilesService.GetAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
    }
}

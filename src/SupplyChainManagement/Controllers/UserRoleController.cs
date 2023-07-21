using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Role()
        {
            return View();
        }

        public IActionResult ChangeRole()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserProfile()
        {
            return View();
        }

        [Authorize]
        [Route("/UserRole/UserProfile/Edit")]
        public IActionResult UserProfileEdit()
        {
            return View();
        }

        [Authorize]
        [Route("/UserRole/UserProfile/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}

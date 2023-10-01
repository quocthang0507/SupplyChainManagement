using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Role()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult ChangeRole(string applciationUserId, string userProfileId)
        {
            ViewBag.ApplciationUserId = applciationUserId;
            ViewBag.UserProfileId = userProfileId;
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        [Route("/UserRole/UserProfile/Edit")]
        [HttpGet]
        public IActionResult UserProfileEdit()
        {
            return View();
        }

        [Route("/UserRole/UserProfile/ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("/UserRole/UserProfiles/Edit")]
        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet]
        public IActionResult UserProfilesEdit()
        {
            return View();
        }
    }
}

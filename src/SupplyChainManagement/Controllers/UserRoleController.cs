using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Controllers.Endpoints;
using SupplyChainManagement.Models.AccountViewModels;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleController _roleController;


        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IRoles roles)
        {
            _userManager = userManager;
            _roleController = new RoleController(_userManager, roleManager, roles);
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
        public async Task<IActionResult> ChangeRole(string applicationUserId)
        {
            var roles = (await _roleController.GetRolesByApplicationUserId(applicationUserId) as OkObjectResult).Value;
            roles = (roles as ApiResponse<RetrievalResponse<UserRoleViewModel>>).Result;
            roles = (roles as RetrievalResponse<UserRoleViewModel>).Items;
            ViewBag.Roles = roles;
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

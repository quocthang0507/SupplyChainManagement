using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Models.AccountViewModels;
using SupplyChainManagement.Models.CRUD;
using System.Net;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize(Roles = RoleNames.Admin)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRoles _roles;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IRoles roles)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roles = roles;
        }

        /// <summary>
        /// Lấy danh sách các vai trò
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles()
        {
            await _roles.GenerateAllExistingRolesAsync();

            List<ApplicationRole> roles = _roleManager.Roles.ToList();
            return Ok(ApiResponse.Success(new RetrievalResponse<ApplicationRole>(roles)));
        }

        /// <summary>
        /// Lấy vai trò theo người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRolesByApplicationUserId([FromRoute] string id)
        {
            await _roles.GenerateAllExistingRolesAsync();
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = _roleManager.Roles.ToList();
                List<UserRoleViewModel> items = new();
                int count = 1;
                foreach (var item in roles)
                {
                    bool isInRole = await _userManager.IsInRoleAsync(user, item.Name);
                    items.Add(new UserRoleViewModel { CounterId = count, ApplicationUserId = id, RoleName = item.Name, IsHaveAccess = isInRole });
                    count++;
                }
                return Ok(ApiResponse.Success(new RetrievalResponse<UserRoleViewModel>(items)));
            }
            return Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy người dùng có id={id}"));
        }

        /// <summary>
        /// Cập nhật vai trò của người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserRole([FromBody] CrudViewModel<UserRoleViewModel> payload)
        {
            UserRoleViewModel userRole = payload.value;
            if (userRole != null)
            {
                var user = await _userManager.FindByIdAsync(userRole.ApplicationUserId);
                if (user != null)
                {
                    if (userRole.IsHaveAccess)
                    {
                        await _userManager.AddToRoleAsync(user, userRole.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                    }
                }
                return Ok(ApiResponse.Success(userRole));
            }
            return Ok(ApiResponse.Fail(HttpStatusCode.BadRequest));
        }
    }
}

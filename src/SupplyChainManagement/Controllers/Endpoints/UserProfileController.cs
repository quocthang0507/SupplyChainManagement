using ApplicationCore.Entities;
using ApplicationCore.ResponseModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Models;
using SupplyChainManagement.Models.CRUD;
using System.Net;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/UserProfile")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserProfilesService _userProfilesService;
        private readonly ApplicationUsersService _applicationUsersService;

        public UserProfileController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, UserProfilesService userProfilesService, ApplicationUsersService applicationUsersService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userProfilesService = userProfilesService;
            _applicationUsersService = applicationUsersService;
        }

        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            List<UserProfile> profiles = await _userProfilesService.GetAsync();
            return Ok(ApiResponse.Success(new RetrievalResponse<UserProfile>(profiles)));
        }

        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPagedUsers")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([AsParameters] PagingModel pagingModel)
        {
            var profiles = await _userProfilesService.GetPagedAsync(pagingModel);
            return Ok(ApiResponse.Success(profiles));
        }

        /// <summary>
        /// Lấy người dùng theo ApplicationUserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByApplicationUserId([FromRoute] string id)
        {
            UserProfile? userProfile = await _userProfilesService.GetByApplicationUserIdAsync(id);
            List<UserProfile> profiles = new();
            if (userProfile != null)
            {
                profiles.Add(userProfile);
            }
            return Ok(ApiResponse.Success(new RetrievalResponse<UserProfile>(profiles)));
        }

        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile register = payload.value;
            if (register.Password.Equals(register.ConfirmPassword))
            {
                ApplicationUser user = new() { Email = register.Email, UserName = register.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    register.Password = user.PasswordHash;
                    register.ConfirmPassword = user.PasswordHash;
                    register.ApplicationUserId = user.Id.ToString();
                    await _userProfilesService.CreateAsync(register);
                }
            }
            return Ok(ApiResponse.Success(register));
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            await _userProfilesService.UpdateAsync(profile.Id, profile);
            return Ok(ApiResponse.Success(profile));
        }

        /// <summary>
        /// Thay đổi mật khẩu người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            if (profile.Password.Equals(profile.ConfirmPassword))
            {
                var user = await _userManager.FindByIdAsync(profile.ApplicationUserId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, profile.OldPassword, profile.Password);
                    if (result.Succeeded)
                        return Ok(ApiResponse.Success("Cập nhật mật khẩu thành công"));
                    return Ok(ApiResponse.Fail(HttpStatusCode.Forbidden, result.Errors.ToArray()));
                }
                return Ok(ApiResponse.Fail(HttpStatusCode.NotFound));
            }
            return Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Mật khẩu và mật khẩu xác nhận không khớp"));
        }

        /// <summary>
        /// Thay đổi vai trò người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ChangeRole([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            return Ok(ApiResponse.Success(profile));
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove([FromBody] CrudViewModel<UserProfile> payload)
        {
            var userProfile = await _userProfilesService.GetAsync(payload.key.ToString());
            if (userProfile != null)
            {
                var user = await _applicationUsersService.GetAsync(userProfile.ApplicationUserId);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await _userProfilesService.DeleteAsync(userProfile.Id);
                    }
                    return Ok();
                }
            }
            return Ok(ApiResponse.Fail(HttpStatusCode.NotFound));
        }
    }
}

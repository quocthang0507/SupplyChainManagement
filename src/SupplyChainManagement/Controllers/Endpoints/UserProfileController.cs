using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SupplyChainManagement.Models;
using SupplyChainManagement.Models.CRUD;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/UserProfile")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserProfilesService _userProfilesService;
        private readonly ApplicationUsersService _applicationUsersService;

        public UserProfileController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, UserProfilesService userProfilesService, ApplicationUsersService applicationUsersService)
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
        public async Task<IActionResult> GetUsers()
        {
            List<UserProfile> Items = await _userProfilesService.GetAsync();
            int Count = Items.Count;
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Lấy người dùng theo ApplicationUserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByApplicationUserId([FromRoute] string id)
        {
            UserProfile? userProfile = await _userProfilesService.GetByApplicationUserIdAsync(id);
            List<UserProfile> Items = new();
            if (userProfile != null)
            {
                Items.Add(userProfile);
            }
            int Count = Items.Count;
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
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
            return Ok(register);
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            await _userProfilesService.UpdateAsync(profile.UserProfileId, profile);
            return Ok(profile);
        }

        /// <summary>
        /// Thay đổi mật khẩu người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            if (profile.Password.Equals(profile.ConfirmPassword))
            {
                var user = await _userManager.FindByIdAsync(profile.ApplicationUserId);
                if (user != null)
                    await _userManager.ChangePasswordAsync(user, profile.OldPassword, profile.Password);
            }
            profile = await _userProfilesService.GetByApplicationUserIdAsync(profile.ApplicationUserId);
            return Ok(profile);
        }

        /// <summary>
        /// Thay đổi vai trò người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult ChangeRole([FromBody] CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            return Ok(profile);
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                        await _userProfilesService.DeleteAsync(userProfile.UserProfileId);
                    }
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}

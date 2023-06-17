using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SupplyChainManagement.Helper;
using SupplyChainManagement.Models;
using SupplyChainManagement.Services;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/UploadProfilePicture")]
    public class UploadProfilePictureController : Controller
    {
        private readonly IFunctional _functionalService;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserProfilesService _userProfilesService;

        public UploadProfilePictureController(IFunctional functionalService, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, UserProfilesService userProfilesService)
        {
            _functionalService = functionalService;
            _env = env;
            _userManager = userManager;
            _userProfilesService = userProfilesService;
        }

        /// <summary>
        /// Tải hình ảnh lên máy chủ
        /// </summary>
        /// <param name="uploadDefault"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(5242880)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUploadProfilePicture(List<IFormFile> uploadDefault)
        {
            try
            {
                if (ImageHelper.IsValidImage(uploadDefault))
                {
                    var folderUpload = "upload";
                    var fileName = await _functionalService.UploadFile(uploadDefault, _env, folderUpload);

                    var appUser = await _userManager.GetUserAsync(User);
                    if (appUser != null)
                    {
                        var profile = await _userProfilesService.GetByApplicationUserIdAsync(appUser.Id.ToString());
                        if (profile != null)
                        {
                            profile.ProfilePicture = "/" + folderUpload + "/" + fileName;
                            await _userProfilesService.UpdateAsync(profile.UserProfileId, profile);
                        }
                    }
                    return Ok(fileName);
                }
                return new UnsupportedMediaTypeResult();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
    }
}

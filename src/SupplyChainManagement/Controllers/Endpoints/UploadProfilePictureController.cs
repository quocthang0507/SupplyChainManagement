using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
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

        [HttpPost]
        [RequestSizeLimit(5242880)]
        public async Task<IActionResult> PostUploadProfilePicture(List<IFormFile> UploadDefault)
        {
            try
            {
                var folderUpload = "upload";
                var fileName = await _functionalService.UploadFile(UploadDefault, _env, folderUpload);

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
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}

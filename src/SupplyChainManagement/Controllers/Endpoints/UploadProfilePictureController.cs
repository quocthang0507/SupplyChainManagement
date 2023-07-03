using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Helper;
using System.Net;

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
        /// <param name="chunkFiles"></param>
        /// <param name="uploadFiles"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(5242880)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostUploadProfilePicture(IList<IFormFile> chunkFiles, IList<IFormFile> uploadFiles)
        {
            try
            {
                if (ImageHelper.IsValidImage(uploadFiles))
                {
                    var folderUpload = "upload";
                    var fileName = await _functionalService.UploadFile(uploadFiles, _env, folderUpload);

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
                    return Ok(ApiResponse.Success(fileName));
                }
                return Ok(ApiResponse.Fail(HttpStatusCode.UnsupportedMediaType, "Không hỗ trợ định dạng này!"));
            }
            catch (Exception e)
            {
                return Ok(ApiResponse.Fail(HttpStatusCode.InternalServerError, $"Lỗi hệ thống, tên lỗi {e.Message}!"));
            }
        }
    }
}

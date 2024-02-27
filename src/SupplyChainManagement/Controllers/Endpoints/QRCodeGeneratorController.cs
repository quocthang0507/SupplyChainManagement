using ApplicationCore.Entities;
using ApplicationCore.Helper;
using ApplicationCore.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Route("api/[controller]")]
    public class QRCodeGeneratorController : Controller
    {
        /// <summary>
        /// Tạo mã QR
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index(QRCodeModel model)
        {
            string uri = QRCodeHelper.Create(model);
            return Ok(ApiResponse.Success(uri));
        }
    }
}

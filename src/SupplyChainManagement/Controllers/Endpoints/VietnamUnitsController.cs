using ApplicationCore.Entities;
using ApplicationCore.ResponseModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VietnamUnitsController : Controller
    {
        private readonly VietnamUnitsService _unitsService;

        public VietnamUnitsController(VietnamUnitsService unitsService)
        {
            _unitsService = unitsService;
        }

        /// <summary>
        /// Lấy danh sách các xã/ phường thuộc tỉnh/ thành phố và huyện/ quận
        /// </summary>
        /// <param name="province">Tên tỉnh/ thành phố</param>
        /// <param name="district">Tên huyện/ quận</param>
        /// <returns>Danh sách tên các xã/ phường</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(string? province, string? district)
        {
            if (province == null)
            {
                var provinces = await _unitsService.GetProvinces();
                return Ok(ApiResponse.Success(new RetrievalResponse<string>(provinces)));
            }
            else if (district == null)
            {
                var districts = await _unitsService.GetDistricts(province);
                return Ok(ApiResponse.Success(new RetrievalResponse<string>(districts)));
            }
            else
            {
                var wards = await _unitsService.GetWards(province, district);
                return Ok(ApiResponse.Success(new RetrievalResponse<string>(wards)));
            }
        }

    }
}

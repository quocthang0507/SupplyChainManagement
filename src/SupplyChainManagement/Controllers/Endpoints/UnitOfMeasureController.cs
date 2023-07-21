using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.ResponseModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Models.CRUD;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize(Roles = RoleNames.Admin)]
    [Produces("application/json")]
    [Route("api/UnitOfMeasure")]
    public class UnitOfMeasureController : Controller
    {
        private readonly UnitOfMeasuresService _unitOfMeasureService;

        public UnitOfMeasureController(UnitOfMeasuresService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        /// <summary>
        /// Lấy danh sách đơn vị đo lường
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitOfMeasures()
        {
            List<UnitOfMeasure> items = await _unitOfMeasureService.GetAsync();
            return Ok(ApiResponse.Success(new RetrievalResponse<UnitOfMeasure>(items)));
        }

        /// <summary>
        /// Thêm đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            UnitOfMeasure unitOfMeasure = payload.value;
            await _unitOfMeasureService.CreateAsync(unitOfMeasure);
            return Ok(ApiResponse.Success(unitOfMeasure));
        }

        /// <summary>
        /// Cập nhật đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            UnitOfMeasure unitOfMeasure = payload.value;
            await _unitOfMeasureService.UpdateAsync(unitOfMeasure.UnitOfMeasureId, unitOfMeasure);
            return Ok(ApiResponse.Success(unitOfMeasure));
        }

        /// <summary>
        /// Xóa đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            await _unitOfMeasureService.DeleteAsync(payload.key.ToString());
            return Ok();
        }
    }
}

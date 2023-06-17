using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SupplyChainManagement.Models;
using SupplyChainManagement.Models.CRUD;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/UnitOfMeasure")]
    public class UnitOfMeasureController : Controller
    {
        private readonly UnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(UnitOfMeasureService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        /// <summary>
        /// Lấy danh sách đơn vị đo lường
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUnitOfMeasures()
        {
            List<UnitOfMeasure> Items = await _unitOfMeasureService.GetAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Thêm đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            UnitOfMeasure unitOfMeasure = payload.value;
            await _unitOfMeasureService.CreateAsync(unitOfMeasure);
            return Ok(unitOfMeasure);
        }

        /// <summary>
        /// Cập nhật đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            UnitOfMeasure unitOfMeasure = payload.value;
            await _unitOfMeasureService.UpdateAsync(unitOfMeasure.UnitOfMeasureId, unitOfMeasure);
            return Ok(unitOfMeasure);
        }

        /// <summary>
        /// Xóa đơn vị đo lường
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            await _unitOfMeasureService.DeleteAsync(payload.key.ToString());
            return NoContent();

        }
    }
}

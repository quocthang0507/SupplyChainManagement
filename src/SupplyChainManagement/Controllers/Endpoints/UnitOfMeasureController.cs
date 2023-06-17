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

        // GET: api/UnitOfMeasure
        [HttpGet]
        public async Task<IActionResult> GetUnitOfMeasure()
        {
            List<UnitOfMeasure> Items = await _unitOfMeasureService.GetAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<UnitOfMeasure> payload)
        {
            UnitOfMeasure unitOfMeasure = payload.value;
            await _unitOfMeasureService.CreateAsync(unitOfMeasure);
            return Ok(unitOfMeasure);
        }
    }
}

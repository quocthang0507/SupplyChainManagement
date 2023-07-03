using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    [Authorize(Roles = MainMenu.UnitOfMeasure.RoleName)]
    public class UnitOfMeasureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

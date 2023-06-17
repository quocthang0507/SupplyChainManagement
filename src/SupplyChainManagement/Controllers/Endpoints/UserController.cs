using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers.Endpoints
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

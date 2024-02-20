using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class SupplyChainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgriculturalProduct()
        {
            return View();
        }

        public IActionResult Farm()
        {
            return View();
        }

        public IActionResult FarmingHousehold()
        {
            return View();
        }

        public IActionResult Transporter()
        {
            return View();
        }

        public IActionResult Retailer()
        {
            return View();
        }
    }
}

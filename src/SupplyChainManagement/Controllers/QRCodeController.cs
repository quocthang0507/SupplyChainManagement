using ApplicationCore.Entities;
using ApplicationCore.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class QRCodeController : Controller
    {
        [HttpGet("/QRCode")]
        public IActionResult Index()
        {
            ViewBag.DarkColor = "#000000";
            ViewBag.LightColor = "#FFFFFF";
            return View();
        }

        [HttpPost]
        public IActionResult Index(QRCodeModel model)
        {
            ViewBag.DarkColor = model.DarkColor;
            ViewBag.LightColor = model.LightColor;
            ViewBag.QRCodeUri = QRCodeHelper.Create(model);
            return View();
        }
    }
}

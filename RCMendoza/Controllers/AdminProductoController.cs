using Microsoft.AspNetCore.Mvc;

namespace RCMendoza.Controllers
{
    public class AdminProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RCMendoza.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Zwemclub.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

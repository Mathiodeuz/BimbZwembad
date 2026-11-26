using Microsoft.AspNetCore.Mvc;

namespace Zwemclub.Controllers
{
    public class BeheerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

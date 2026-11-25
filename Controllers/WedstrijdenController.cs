namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class WedstrijdenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
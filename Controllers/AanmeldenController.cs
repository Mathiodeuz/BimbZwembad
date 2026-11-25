namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class AanmeldenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
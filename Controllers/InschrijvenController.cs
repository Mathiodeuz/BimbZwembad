namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class InschrijvenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
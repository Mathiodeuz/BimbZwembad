namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
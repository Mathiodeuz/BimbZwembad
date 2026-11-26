using Zwemclub.DTO.EmailDTOs;
using Zwemclub.Services.EmailService;

namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class HomeController(UserManager<CustomUser> userManager) : Controller
    {
       
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await userManager.GetUserAsync(User);
                ViewBag.Voornaam = user?.Voornaam;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [ActionName("StatusCode")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpStatusCode(int code)
        {
            Response.StatusCode = code is >= 400 and <= 599 ? code : StatusCodes.Status404NotFound;
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(Response.StatusCode == StatusCodes.Status404NotFound ? "NotFound" : "Error");
        }

        public IActionResult ToegangGeweigerd()
        {
            return View();
        }
    }
}

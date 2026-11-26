using Zwemclub.Services.RegistratieService;
using Zwemclub.Utils;
using Zwemclub.ViewModels;

namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager,IRegistratieService registratieService) : Controller
    {
        private const string SessieSleutel = "Registratie";
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View(model);



            var user = await userManager.FindByEmailAsync(model.Email);

           

            if (user is null)   
            {
                ModelState.AddModelError("", "Account niet gevonden.");
                return View(model);
            }

            if (!user.IsActief || user.IsVerwijderd == true)
            {
                ModelState.AddModelError("", "Dit account is gedeactiveerd.");
                return View(model);
            }

            var result = await signInManager.CheckPasswordSignInAsync(
                user, model.Wachtwoord, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.IsLockedOut
                    ? "Te veel mislukte pogingen. Probeer later opnieuw."
                    : "Onjuist wachtwoord.");
                return View(model);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            Debug.WriteLine($"Signed in: {signInManager.IsSignedIn(User)}");

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return await RedirectNaarDashboard(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<IActionResult> RedirectNaarDashboard(CustomUser user)
        {
            var rollen = await userManager.GetRolesAsync(user);

            if (rollen.Contains("Voorzitter")) return RedirectToAction("Index", "Beheer");
            if (rollen.Contains("Trainer")) return RedirectToAction("Index", "Trainer");
            return RedirectToAction("Index", "Home");  
        }
        [HttpGet]
        public IActionResult KiesRol() => View();
        [HttpGet]
        public IActionResult Register(string rol)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            if (rol is not ("Zwemmer" or "Ouder"))
                return RedirectToAction(nameof(KiesRol));
            return View(new RegistratieViewModel { Rol = rol });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistratieViewModel model)
        {
            if (model.Rol is not ("Zwemmer" or "Ouder"))
                return RedirectToAction(nameof(KiesRol));

            if (model.Rol == "Zwemmer" && model.Geboortedatum is null)
                ModelState.AddModelError(nameof(model.Geboortedatum), "Geboortedatum is verplicht voor een zwemmer.");

            if (ModelState.IsValid && await userManager.FindByEmailAsync(model.Email) is not null)
                ModelState.AddModelError(nameof(model.Email), "Dit e-mailadres is al in gebruik.");

            if (!ModelState.IsValid)
                return View(model);
            HttpContext.Session.SetJson(SessieSleutel, model);
            return RedirectToAction(nameof(Bevestig));
        }

        [HttpGet]
        public IActionResult Bevestig()
        {
            var model = HttpContext.Session.GetJson<RegistratieViewModel>(SessieSleutel);
            return model is null ? RedirectToAction(nameof(KiesRol)) : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BevestigPost()
        {
            var model = HttpContext.Session.GetJson<RegistratieViewModel>(SessieSleutel);
            if (model is null)
                return RedirectToAction(nameof(KiesRol));

            var result = await registratieService.RegistreerAsync(model);

            if (!result.Succeeded)
            {
                foreach (var fout in result.Errors)
                    ModelState.AddModelError("", fout.Code == "DuplicateEmail"
                        ? "Dit e-mailadres is al in gebruik."
                        : fout.Description);

                return View(nameof(Bevestig), model);   
            }

            HttpContext.Session.Remove(SessieSleutel);

            var user = await userManager.FindByEmailAsync(model.Email);
            await signInManager.SignInAsync(user!, isPersistent: false);
            return await RedirectNaarDashboard(user!);
        }

        public IActionResult GeenToegang()=>RedirectToAction("ToegangGeweigerd","Home");


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.Users.Include(u => u.Geslacht).FirstOrDefaultAsync(u=>u.Id == userManager.GetUserId(User)); ;

            if (user is null)
                return Challenge();

            var model = new EditProfielViewModel
            {
                FederatieNummer = user.FederatieNummer ?? "",
                UniekNummer = user.UniekNummer,
                Voornaam = user.Voornaam ?? "",
                Naam = user.Naam ?? "",
                Email = user.Email ?? "",
                Telefoon = user.PhoneNumber,
                Geboortedatum = user.Geboortedatum,
                GeslachtsNaam = user.Geslacht?.Naam
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfielViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return Challenge();

            user.Voornaam = model.Voornaam;
            user.Naam = model.Naam;
            user.PhoneNumber = model.Telefoon;
            user.Geboortedatum = model.Geboortedatum;


            var emailResult = await userManager.SetEmailAsync(user, model.Email);

            if (!emailResult.Succeeded)
            {
                foreach (var error in emailResult.Errors)
                    ModelState.AddModelError(nameof(model.Email), error.Description);

                return View(model);
            }

            
            var usernameResult = await userManager.SetUserNameAsync(user, model.Email);

            if (!usernameResult.Succeeded)
            {
                foreach (var error in usernameResult.Errors)
                    ModelState.AddModelError(nameof(model.Email), error.Description);

                return View(model);
            }

            var updateResult = await userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            TempData["Success"] = "Je profiel werd succesvol bijgewerkt.";

            return RedirectToAction(nameof(Edit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return Challenge();


            user.Voornaam = "<<Verwijderd>>";
            user.Naam = "<<Verwijderd>>";
            user.Email = $"deleted_{user.Id}@deleted.local";
            user.UserName = $"deleted_{user.Id}";
            user.UniekNummer = $"<<Verwijderd>>_{user.Id}";

            
            user.PhoneNumber = null;
            user.Geboortedatum = null;
            user.GeslachtId = null;
            user.FederatieNummer = null;
            user.ProfielFoto = null;


            user.IsActief = false;
            user.IsVerwijderd = true;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Er ging iets mis bij het verwijderen van je account.";
                return RedirectToAction(nameof(Edit));
            }

            
            var rollen = await userManager.GetRolesAsync(user);
            var roleResult = await userManager.RemoveFromRolesAsync(user, rollen);

            if (!roleResult.Succeeded)
            {
                TempData["Error"] = "De account werd aangepast, maar de rol kon niet worden verwijderd.";
                return RedirectToAction(nameof(Edit));
            }

            
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
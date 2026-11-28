using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Zwemclub.DTO.EmailDTOs;
using Zwemclub.Services.EmailService;
using Zwemclub.Services.RegistratieService;
using Zwemclub.Utils;
using Zwemclub.ViewModels;

namespace Zwemclub.Controllers
{
    [AllowAnonymous]
    public class AccountController(UserManager<CustomUser> userManager, 
        SignInManager<CustomUser> signInManager,
        IRegistratieService registratieService,
        IWebHostEnvironment environment,
        IEmailService emailService) : Controller
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
            var testUser = new CustomUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            foreach (var validator in userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(
                    userManager,
                    testUser,
                    model.Wachtwoord
                );

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(nameof(model.Wachtwoord), error.Description);
                    }
                }
            }

            
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (model.ProfielFoto is not null)
            {
                var foto = model.ProfielFoto;

                if (foto.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError(nameof(model.ProfielFoto),
                        "De afbeelding mag maximaal 5 MB groot zijn.");

                    return View(model);
                }

                var toegestaneExtensies = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extensie = Path.GetExtension(foto.FileName).ToLowerInvariant();

                if (!toegestaneExtensies.Contains(extensie))
                {
                    ModelState.AddModelError(nameof(model.ProfielFoto),
                        "Gebruik een JPG, PNG of WEBP afbeelding.");

                    return View(model);
                }

                var tijdelijkeMap = Path.Combine(environment.ContentRootPath, "App_Data", "TempProfielFotos");

                Directory.CreateDirectory(tijdelijkeMap);

                var bestandsnaam = $"{Guid.NewGuid()}{extensie}";
                var bestandspad = Path.Combine(tijdelijkeMap, bestandsnaam);

                using (var stream = new FileStream(bestandspad, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                model.ProfielFotoPad = bestandspad;
            }



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
            var user = await userManager.GetUserAsync(User); ;

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
                GeslachtId = user.GeslachtId,
                HuidigeProfielFoto = user.ProfielFoto
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

            user.GeslachtId = model.GeslachtId;

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

            if (model.ProfielFotoUpload is not null
                && model.ProfielFotoUpload.Length > 0)
            {
                var foto = model.ProfielFotoUpload;

                var toegestaneExtensies = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extensie = Path.GetExtension(foto.FileName).ToLowerInvariant();

                if (foto.Length > 5 * 1024 * 1024 ||
                    !toegestaneExtensies.Contains(extensie))
                {
                    ModelState.AddModelError(
                        nameof(model.ProfielFotoUpload),
                        "De foto moet een JPG, PNG of WEBP zijn en maximaal 5 MB groot.");

                    model.HuidigeProfielFoto = user.ProfielFoto;
                    return View(model);
                }

                var uploadMap = Path.Combine(
                    environment.WebRootPath,
                    "uploads",
                    "profiel");

                Directory.CreateDirectory(uploadMap);

                var bestandsnaam = $"{Guid.NewGuid()}{extensie}";
                var bestandspad = Path.Combine(uploadMap, bestandsnaam);

                using (var stream = new FileStream(bestandspad, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                
                if (!string.IsNullOrEmpty(user.ProfielFoto))
                {
                    var oudPad = Path.Combine(
                        environment.WebRootPath,
                        user.ProfielFoto.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                    if (System.IO.File.Exists(oudPad))
                        System.IO.File.Delete(oudPad);
                }

                
                user.ProfielFoto = $"/uploads/profiel/{bestandsnaam}";
            }



            var updateResult = await userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            TempData["Success"] = "Je profiel werd succesvol bijgewerkt.";

            return RedirectToAction(nameof(Profiel));
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
            if (!string.IsNullOrEmpty(user.ProfielFoto))
            {
                var fotoPad = Path.Combine(
                    environment.WebRootPath,
                    user.ProfielFoto.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(fotoPad))
                    System.IO.File.Delete(fotoPad);
            }
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

        [HttpGet]
        public async Task<IActionResult> Profiel()
        {

            var userId = userManager.GetUserId(User);

            var user = await userManager.Users
                .Include(u => u.Geslacht)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Challenge();

            var model = new ProfielViewModel
            {
                Voornaam = user.Voornaam ?? "",
                Naam = user.Naam ?? "",
                Email = user.Email ?? "",
                Telefoon = user.PhoneNumber,
                Geboortedatum = user.Geboortedatum,
                GeslachtsNaam = user.Geslacht?.Naam,
                UniekNummer = user.UniekNummer,
                FederatieNummer = user.FederatieNummer,
                ProfielFoto = user.ProfielFoto
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Forgot()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(
        WachtwoordVergetenViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);

            
            if (user is not null && user.IsActief && user.IsVerwijderd != true)
            {
                
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                
                
                var encodedToken = WebEncoders.Base64UrlEncode(
                    Encoding.UTF8.GetBytes(token)
                );

                
                var resetLink = Url.Action(
                    nameof(Reset),
                    "Account",
                    new
                    {
                        email = user.Email,
                        token = encodedToken
                    },
                    protocol: Request.Scheme
                );

                
                var email = new EmailDTO
                {
                    To = user.Email!,
                    Subject = "Wachtwoord opnieuw instellen",
                    Body = $"""
                <h2>Wachtwoord opnieuw instellen</h2>

                <p>Beste {HtmlEncoder.Default.Encode(user.Voornaam ?? "gebruiker")},</p>

                <p>
                    Je hebt aangevraagd om je wachtwoord opnieuw in te stellen.
                    Klik op de onderstaande link om een nieuw wachtwoord te kiezen.
                </p>

                <p>
                    <a href="{resetLink}">
                        Nieuw wachtwoord instellen
                    </a>
                </p>

                <p>
                    Heb je deze aanvraag niet gedaan?
                    Dan mag je deze e-mail gewoon negeren.
                </p>

                <p>Met vriendelijke groeten,<br/>Zwemclub</p>
                """
                };

                
                await emailService.SendEmailAsync(email);
            }


            TempData["Success"] =
                "Als dit e-mailadres bij een account hoort, ontvang je een e-mail met een link om je wachtwoord opnieuw in te stellen.";

            return RedirectToAction(nameof(BevestigingForgot));
        }


        
        [HttpGet]
        public IActionResult BevestigingForgot()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Reset(string email, string token)
        {
            try
            {
                var decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(token)
                );
                

                var model = new WachtwoordResettenViewModel
                {
                    Email = email,
                    Token = decodedToken
                };
                //anders pakt deze de base64-encoded token die in Forgot post.
                ModelState.Remove(nameof(model.Token));

                return View(model);
            }
            catch (FormatException)
            {
                return View("ResetLinkOngeldig");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(
        WachtwoordResettenViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("",
                    "De resetlink is ongeldig of verlopen.");

                return View(model);
            }


            var result = await userManager.ResetPasswordAsync(
                user,
                model.Token,
                model.NieuwWachtwoord
            );

            


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        nameof(model.NieuwWachtwoord),
                        error.Description
                    );
                }

                return View(model);
            }

            TempData["Success"] = "Je wachtwoord werd succesvol gewijzigd.";

            return RedirectToAction(nameof(Login));
        }
    }
}
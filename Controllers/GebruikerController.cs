
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zwemclub.Data.UnitOfWork;
using Zwemclub.Models;
using Zwemclub.ViewModels.Gebruiker;

namespace Zwemclub.Controllers
{
    [Route("[controller]/[action]")]
    public class GebruikerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public GebruikerController(
            IUnitOfWork unitOfWork,
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ======================
        // LOGIN
        // ======================

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user =
                await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Account niet gevonden.");

                return View(model);
            }

            if (!user.IsActief)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Account is niet actief.");

                return View(model);
            }

            var result =
                await _signInManager.PasswordSignInAsync(
                    user.UserName!,
                    model.Password,
                    model.RememberMe,
                    false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Ongeldig wachtwoord.");

                return View(model);
            }

            return RedirectToAction(
                "Index",
                "Home");
        }

        // ======================
        // LOGOUT
        // ======================

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                nameof(Login));
        }

        // ======================
        // REGISTER
        // ======================

        [HttpGet]
        public IActionResult Register()
        {
            return View(
                new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(
            RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser =
                await _userManager
                    .FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    nameof(model.Email),
                    "E-mailadres bestaat al.");

                return View(model);
            }

            var confirm =
        new RegisterConfirmViewModel
        {
            Voornaam = model.Voornaam,
            Achternaam = model.Achternaam,
            Email = model.Email,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword,

            Geboortedatum = model.Geboortedatum,
            FederatieNummer = model.FederatieNummer,
            GeslachtId = model.GeslachtId
        };

            return View(
                "RegisterConfirm",
                confirm);
        }

        // ======================
        // REGISTER CONFIRM
        // ======================

        [HttpPost]
        public async Task<IActionResult> Complete(
            RegisterConfirmViewModel model)
        {
            if (!ModelState.IsValid)
                return View(
                    "RegisterConfirm",
                    model);

            var user = new CustomUser
            {
                UserName = model.Email,
                Email = model.Email,

                Voornaam = model.Voornaam,
                Naam = model.Achternaam,

                Geboortedatum = model.Geboortedatum,
                FederatieNummer = model.FederatieNummer,

                IsActief = true,

                UniekNummer = Guid.NewGuid().ToString()
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }

                return View(
                    "RegisterConfirm",
                    model);
            }

            await _signInManager.SignInAsync(
                user,
                false);

            return RedirectToAction(
                "Index",
                "Home");
        }
    }
}
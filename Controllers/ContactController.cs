using Zwemclub.Models;
using Zwemclub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Zwemclub.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IUnitOfWork unitOfWork, ILogger<ContactController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {

            if (!string.IsNullOrWhiteSpace(model.Website))
            {
                TempData["ContactSucces"] = "Bedankt voor je bericht! We nemen zo snel mogelijk contact met je op.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ipAdres = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrWhiteSpace(ipAdres))
            {
                var grens = DateTime.UtcNow.AddMinutes(-10);
                var alleBerichten = await _unitOfWork.ContactBerichten.GetAllAsync();
                var recenteVanIp = alleBerichten.Count(b => b.AfzenderIp == ipAdres && b.VerzondenOp >= grens);

                if (recenteVanIp >= 3)
                {
                    ModelState.AddModelError(string.Empty, "Je hebt onlangs al meerdere berichten verzonden. Probeer het later opnieuw.");
                    return View(model);
                }
            }

            var bericht = new ContactBericht
            {
                Naam = OpschoonTekst(model.Naam),
                Email = model.Email.Trim().ToLowerInvariant(),
                Onderwerp = OpschoonTekst(model.Onderwerp),
                Bericht = OpschoonTekst(model.Bericht),
                AfzenderIp = ipAdres,
                VerzondenOp = DateTime.UtcNow,
                IsBeantwoord = false
            };

            try
            {
                await _unitOfWork.ContactBerichten.AddAsync(bericht);
                await _unitOfWork.SaveChangesAsync();

                TempData["ContactSucces"] = "Bedankt voor je bericht! Ons clubbestuur neemt spoedig contact met je op.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij opslaan van contactbericht.");
                ModelState.AddModelError(string.Empty, "Er ging iets mis bij het verzenden van je bericht.");
                return View(model);
            }
        }

        private static string OpschoonTekst(string input)
        {
            return input.Trim().Replace("<", string.Empty).Replace(">", string.Empty);
        }
    }
}

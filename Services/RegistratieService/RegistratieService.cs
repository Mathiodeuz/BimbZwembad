using Zwemclub.ViewModels;

namespace Zwemclub.Services.RegistratieService
{
    public class RegistratieService(UserManager<CustomUser> userManager, 
        IUnitOfWork unitOfWork,
        IWebHostEnvironment environment) : IRegistratieService
    {
        public async Task<IdentityResult> RegistreerAsync(RegistratieViewModel model)
        {
            if (model.Rol is not ("Zwemmer" or "Ouder"))
                return IdentityResult.Failed(new IdentityError { Description = "Ongeldige rol." });
            var nummer = await unitOfWork.Gebruikers.GetVolgendeUniekNummerAsync();

            var geslachten = await unitOfWork.Geslachten.GetAllAsync();

            var geslachtBestaat = geslachten.Any(g =>
                g.Id == model.GeslachtId && !g.IsVerwijderd);

            if (!geslachtBestaat)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidGender",
                    Description = "Ongeldig geslacht geselecteerd."
                });
            }

            var user = new CustomUser
            {
                UserName = model.Email,          
                Email = model.Email,
                PhoneNumber = model.Telefoon,
                Voornaam = model.Voornaam,
                Naam = model.Naam,
                ProfielFoto = model.ProfielFotoPad,
                Geboortedatum = model.Geboortedatum,
                GeslachtId = model.GeslachtId!.Value,
                UniekNummer = $"ZC-{nummer:D4}",
                IsActief = true,                 
                IsVerwijderd = false
            };

            if (!string.IsNullOrEmpty(model.ProfielFotoPad)
                && File.Exists(model.ProfielFotoPad))
            {
                var uploadMap = Path.Combine(
                    environment.WebRootPath,
                    "uploads",
                    "profiel");

                Directory.CreateDirectory(uploadMap);

                var extensie = Path.GetExtension(model.ProfielFotoPad);
                var bestandsnaam = $"{Guid.NewGuid()}{extensie}";

                var definitiefPad = Path.Combine(uploadMap, bestandsnaam);

                File.Move(model.ProfielFotoPad, definitiefPad);

                user.ProfielFoto = $"/uploads/profiel/{bestandsnaam}";
            }



            var result = await userManager.CreateAsync(user, model.Wachtwoord);
            if (!result.Succeeded)
                return result;

            var rolResult = await userManager.AddToRoleAsync(user, model.Rol);
            if (!rolResult.Succeeded)
            {
                await userManager.DeleteAsync(user);   
                return rolResult;
            }

            return IdentityResult.Success;
        }
    }
    }


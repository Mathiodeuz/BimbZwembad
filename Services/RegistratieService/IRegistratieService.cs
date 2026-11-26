using Zwemclub.ViewModels;

namespace Zwemclub.Services.RegistratieService
{
    public interface IRegistratieService
    {
        Task<IdentityResult> RegistreerAsync(RegistratieViewModel model);
    }
}

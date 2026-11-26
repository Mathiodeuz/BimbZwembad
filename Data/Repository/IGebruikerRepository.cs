using Zwemclub.Models;

namespace Zwemclub.Data.Repository
{
    public interface IGebruikerRepository
        : IGenericRepository<CustomUser>
    {
        Task<CustomUser?> FindByEmailAsync(string email);
    }
}

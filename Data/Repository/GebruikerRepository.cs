using Microsoft.EntityFrameworkCore;
using Zwemclub.Models;

namespace Zwemclub.Data.Repository
{
    public class GebruikerRepository
        : GenericRepository<CustomUser>,
          IGebruikerRepository
    {
        private readonly ZwemclubContext _context;

        public GebruikerRepository(ZwemclubContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<CustomUser?> FindByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

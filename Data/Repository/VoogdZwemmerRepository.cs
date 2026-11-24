namespace Zwemclub.Data.Repository
{
    public class VoogdZwemmerRepository(ZwemclubContext context) : GenericRepository<VoogdZwemmer>(context), IVoogdZwemmerRepository
    {
        public async Task<bool> BestaatAsync(string voogdId, string zwemmerId)
        {
            return await context.VoogdZwemmers
                .AnyAsync(v => v.VoogdId == voogdId && v.ZwemmerId == zwemmerId);
        }

        public async Task<IEnumerable<VoogdZwemmer>> GetKinderenAsync(string voogdId)
        {
            return await context.VoogdZwemmers
                .Include(v => v.Zwemmer)
                .Where(v => v.VoogdId == voogdId)
                .ToListAsync();
        }

        public async Task<VoogdZwemmer?> GetKoppelingAsync(string voogdId, string zwemmerId)
        {
            return await context.VoogdZwemmers
                .FirstOrDefaultAsync(v => v.VoogdId == voogdId && v.ZwemmerId == zwemmerId);
        }

        public async Task<IEnumerable<VoogdZwemmer>> GetKoppelingenZonderMailAsync()
        {
            return await context.VoogdZwemmers
                .Include(v => v.Voogd)
                .Include(v => v.Zwemmer)
                .Where(v => v.IsMailGestuurd != true)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoogdZwemmer>> GetVoogdenAsync(string zwemmerId)
        {
            return await context.VoogdZwemmers
                .Include(v => v.Voogd)
                .Where(v => v.ZwemmerId == zwemmerId)
                .ToListAsync();
        }
    }
}

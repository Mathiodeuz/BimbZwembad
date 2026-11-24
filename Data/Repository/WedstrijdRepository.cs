namespace Zwemclub.Data.Repository
{
    public class WedstrijdRepository(ZwemclubContext context) : GenericRepository<Wedstrijd>(context), IWedstrijdRepository
    {
        
        public async Task<IEnumerable<Wedstrijd>> GetByJaarAsync(int jaar)
        {
            return await context.Wedstrijden
                .Include(w => w.Plaats)
                .Where(w => w.Datum.Year == jaar)
                .OrderBy(w => w.Datum)
                .ToListAsync();
        }

        public async Task<IEnumerable<Wedstrijd>> GetOpenVoorInschrijvingAsync()
        {
            return await context.Wedstrijden
                .Where(w => w.DeadlineInschrijven >= DateTime.Now)
                .OrderBy(w => w.DeadlineInschrijven)
                .ToListAsync();
        }

        public async Task<Wedstrijd?> GetPubliekAsync(int id)
        {
            return await context.Wedstrijden
                .Include(w => w.Plaats)
                .Include(w => w.Reeksen).ThenInclude(r => r.ReeksType)
                .Include(w => w.Reeksen)
                    .ThenInclude(r => r.Inschrijvingen
                        .Where(i => i.DatumDefinitief != null && i.DatumAnnulatie == null))
                    .ThenInclude(i => i.Zwemmer)
                .AsSplitQuery()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wedstrijd?> GetWithReeksenAsync(int id)
        {
            return await context.Wedstrijden
                .Include(w => w.Plaats)
                .Include(w => w.Reeksen)
                .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}

namespace Zwemclub.Data.Repository
{
    public class WedstrijdReeksRepository(ZwemclubContext context) : GenericRepository<WedstrijdReeks>(context), IWedstrijdReeksRepository
    {
        public async Task<int> GetAantalInschrijvingenAsync(int reeksId)
        {
            return await context.InschrijvingWedstrijden
            .CountAsync(i => i.WedstrijdReeksId == reeksId && i.DatumAnnulatie == null && !i.IsVerwijderd);
        }

        public async Task<IEnumerable<WedstrijdReeks>> GetByWedstrijdAsync(int wedstrijdId)
        {
            return await context.WedstrijdReeksen
            .Include(r => r.ReeksType)
            .Include(r => r.Geslacht)
            .Where(r => r.WedstrijdId == wedstrijdId)
            .OrderBy(r => r.Startuur)
            .ToListAsync();
        }
    }
}

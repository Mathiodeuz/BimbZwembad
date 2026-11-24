namespace Zwemclub.Data.Repository
{
    public class InschrijvingWedstrijdRepository(ZwemclubContext context):GenericRepository<InschrijvingWedstrijd>(context),IInschrijvingWedstrijdRepository
    {
        public async Task<IEnumerable<InschrijvingWedstrijd>> GetByZwemmerAsync(string zwemmerId)
        {
            return await context.InschrijvingWedstrijden
                .Include(i => i.WedstrijdReeks)
                .Where(i => i.ZwemmerId == zwemmerId && !i.IsVerwijderd)
                .ToListAsync();
        }

        
        public async Task<IEnumerable<InschrijvingWedstrijd>> GetByWedstrijdReeksAsync(int wedstrijdReeksId)
        {
            return await context.InschrijvingWedstrijden
                .Include(i => i.Zwemmer)
                .Where(i => i.WedstrijdReeksId == wedstrijdReeksId && !i.IsVerwijderd)
                .ToListAsync();
        }

        
        public async Task<IEnumerable<InschrijvingWedstrijd>> GetDefinitieveByWedstrijdAsync(int wedstrijdId)
        {
            return await context.InschrijvingWedstrijden
                .Include(i => i.Zwemmer)
                .Include(i => i.WedstrijdReeks)
                .Where(i => i.WedstrijdReeks.WedstrijdId == wedstrijdId
                         && i.DatumDefinitief != null
                         && i.DatumAnnulatie == null
                         && !i.IsVerwijderd)
                .ToListAsync();
        }

        
        public async Task<bool> BestaatAsync(string zwemmerId, int wedstrijdReeksId)
        {
            return await context.InschrijvingWedstrijden
                .AnyAsync(i => i.ZwemmerId == zwemmerId
                            && i.WedstrijdReeksId == wedstrijdReeksId
                            && i.DatumAnnulatie == null
                            && !i.IsVerwijderd);
        }
    }
}

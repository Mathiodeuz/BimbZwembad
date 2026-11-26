namespace Zwemclub.Data.Repository
{
    public class GebruikerRepository(ZwemclubContext context) : GenericRepository<CustomUser>(context), IGebruikerRepository
    {
        public async Task<IEnumerable<CustomUser>> GetActieveZwemmersAsync()
        {
            return await (from u in context.Users
                          join ur in context.UserRoles on u.Id equals ur.UserId
                          join r in context.Roles on ur.RoleId equals r.Id
                          where r.Name == "Zwemmer" && u.IsActief && u.IsVerwijderd != true
                          select u).ToListAsync();
        }

        public async Task<IEnumerable<CustomUser>> GetActieveZwemmersZonderCoachAsync()
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Today);

            var metCoach = context.TrainerGroepen
                .Where(t => t.DatumVanaf <= vandaag && t.DatumTot >= vandaag)
                .Select(t => t.ZwemmerId);

            return await (from u in context.Users
                          join ur in context.UserRoles on u.Id equals ur.UserId
                          join r in context.Roles on ur.RoleId equals r.Id
                          where r.Name == "Zwemmer"
                                && u.IsActief
                                && u.IsVerwijderd != true
                                && !metCoach.Contains(u.Id)
                          select u).ToListAsync();
        }

        public async Task<CustomUser?> GetByFederatieNummerAsync(string federatieNummer)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.FederatieNummer == federatieNummer
                                       && u.IsVerwijderd != true);
        }

        public async Task<IEnumerable<CustomUser>> GetByRolAsync(string rolNaam)
        {
            return await (from u in context.Users
                          join ur in context.UserRoles on u.Id equals ur.UserId
                          join r in context.Roles on ur.RoleId equals r.Id
                          where r.Name == rolNaam && u.IsVerwijderd != true
                          select u).ToListAsync();
        }

        public async Task<CustomUser?> GetByUniekNummerAsync(string uniekNummer, DateOnly geboortedatum)
        {
            return await context.Users
               .FirstOrDefaultAsync(u => u.UniekNummer == uniekNummer
                                      && u.Geboortedatum == geboortedatum
                                      && u.IsVerwijderd != true);
        }

        public async Task<int> GetVolgendeUniekNummerAsync()
        {
            //ignorequeryfilters omdat er een filter staat en anders de verwijderde niet meegeteld zouden worden.
            var nummers = await context.Users
                .IgnoreQueryFilters()
                .Select(u => u.UniekNummer)
                .ToListAsync();
            //ga in de lijst en zoek naar de numerieke huidige maximum waarde van de code en doe plus 1.
            return nummers
                .Where(n => n.StartsWith("ZC-") && int.TryParse(n[3..], out _))
                .Select(n => int.Parse(n[3..]))
                .DefaultIfEmpty(0)
                .Max() + 1;
        }

        public async Task<IEnumerable<CustomUser>> ZoekOpNaamAsync(string zoekterm)
        {
            return await context.Users
                .Where(u => u.IsVerwijderd != true
                         && ((u.Naam != null && u.Naam.Contains(zoekterm))
                          || (u.Voornaam != null && u.Voornaam.Contains(zoekterm))))
                .ToListAsync();
        }
    }
}

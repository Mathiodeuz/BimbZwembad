namespace Zwemclub.Data.Repository
{
    public interface IGebruikerRepository:IGenericRepository<CustomUser>
    {
        Task<CustomUser?> GetByUniekNummerAsync(string uniekNummer, DateOnly geboortedatum);
        Task<CustomUser?> GetByFederatieNummerAsync(string federatieNummer);
        Task<IEnumerable<CustomUser>> GetByRolAsync(string rolNaam);
        Task<IEnumerable<CustomUser>> GetActieveZwemmersAsync();
        Task<IEnumerable<CustomUser>> GetActieveZwemmersZonderCoachAsync();
        Task<IEnumerable<CustomUser>> ZoekOpNaamAsync(string zoekterm);
    }
}

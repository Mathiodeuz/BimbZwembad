namespace Zwemclub.Data.Repository
{
    public interface IInschrijvingWedstrijdRepository:IGenericRepository<InschrijvingWedstrijd>
    {
        Task<IEnumerable<InschrijvingWedstrijd>> GetByZwemmerAsync(string zwemmerId);
        Task<IEnumerable<InschrijvingWedstrijd>> GetByWedstrijdReeksAsync(int wedstrijdReeksId);
        Task<IEnumerable<InschrijvingWedstrijd>> GetDefinitieveByWedstrijdAsync(int wedstrijdId);
        Task<bool> BestaatAsync(string zwemmerId, int wedstrijdReeksId);
    }


}

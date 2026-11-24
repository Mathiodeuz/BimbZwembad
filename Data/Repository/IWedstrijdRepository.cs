namespace Zwemclub.Data.Repository
{
    public interface IWedstrijdRepository:IGenericRepository<Wedstrijd>
    {
        Task<IEnumerable<Wedstrijd>> GetByJaarAsync(int jaar);
        Task<Wedstrijd?> GetWithReeksenAsync(int id);
        Task<IEnumerable<Wedstrijd>> GetOpenVoorInschrijvingAsync();
        Task<Wedstrijd?> GetPubliekAsync(int id);
    }
}

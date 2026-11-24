namespace Zwemclub.Data.Repository
{
    public interface IWedstrijdReeksRepository:IGenericRepository<WedstrijdReeks>
    {
        Task<IEnumerable<WedstrijdReeks>> GetByWedstrijdAsync(int wedstrijdId);
        Task<int> GetAantalInschrijvingenAsync(int reeksId);
    }
}

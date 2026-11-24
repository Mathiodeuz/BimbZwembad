namespace Zwemclub.Data.Repository
{
    public interface IVoogdZwemmerRepository:IGenericRepository<VoogdZwemmer>
    {
        Task<IEnumerable<VoogdZwemmer>> GetKinderenAsync(string voogdId);
        Task<IEnumerable<VoogdZwemmer>> GetVoogdenAsync(string zwemmerId);
        Task<VoogdZwemmer?> GetKoppelingAsync(string voogdId, string zwemmerId);
        Task<bool> BestaatAsync(string voogdId, string zwemmerId);
        Task<IEnumerable<VoogdZwemmer>> GetKoppelingenZonderMailAsync();
    }
}

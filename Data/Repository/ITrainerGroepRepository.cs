namespace Zwemclub.Data.Repository
{
    public interface ITrainerGroepRepository:IGenericRepository<TrainerGroep>
    {
        Task<IEnumerable<TrainerGroep>> GetActieveZwemmersAsync(string trainerId);
        Task<IEnumerable<TrainerGroep>> GetHistoriekAsync(string zwemmerId);
        Task<TrainerGroep?> GetActieveKoppelingAsync(string trainerId, string zwemmerId);
    }
}

namespace Zwemclub.Data.Repository
{
    public class TrainerGroepRepository(ZwemclubContext context) : GenericRepository<TrainerGroep>(context), ITrainerGroepRepository
    {
        public async Task<TrainerGroep?> GetActieveKoppelingAsync(string trainerId, string zwemmerId)
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Today);

            return await context.TrainerGroepen
                .FirstOrDefaultAsync(t => t.TrainerId == trainerId
                                       && t.ZwemmerId == zwemmerId
                                       && t.DatumVanaf <= vandaag
                                       && t.DatumTot >= vandaag);
        }

        public async Task<IEnumerable<TrainerGroep>> GetActieveZwemmersAsync(string trainerId)
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Today);

            return await context.TrainerGroepen
                .Include(t => t.Zwemmer)
                .Where(t => t.TrainerId == trainerId
                         && t.DatumVanaf <= vandaag
                         && t.DatumTot >= vandaag)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerGroep>> GetHistoriekAsync(string zwemmerId)
        {
            return await context.TrainerGroepen
                .Include(t => t.Trainer)
                .Where(t => t.ZwemmerId == zwemmerId)
                .OrderByDescending(t => t.DatumVanaf)
                .ToListAsync();
        }
    }
}

namespace Zwemclub.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGebruikerRepository Gebruikers { get; }
        ITrainerGroepRepository TrainerGroepen { get; }
        IInschrijvingWedstrijdRepository InschrijvingWedstrijden { get; }
        IWedstrijdReeksRepository WedstrijdReeksen { get; }
        IVoogdZwemmerRepository VoogdZwemmers { get; }
        IWedstrijdRepository Wedstrijden { get; }
        IGenericRepository<Parameter> Parameters { get; }
        IGenericRepository<ReeksType> ReeksTypes { get; }
        IGenericRepository<Mail> Mails { get; }
        IGenericRepository<Plaats> Plaatsen { get; }
        IGenericRepository<Geslacht> Geslachten { get; }

        Task SaveChangesAsync();
    }
}

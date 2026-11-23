namespace Zwemclub.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<CustomUser> Gebruikers { get; }
        IGenericRepository<GebruikerCarpool> GebruikerCarpools { get; }
        IGenericRepository<Parameter> Parameters { get; }
        IGenericRepository<TrainerGroep> TrainerGroepen { get; }
        IGenericRepository<ReeksType> ReeksTypes { get; }
        IGenericRepository<InschrijvingWedstrijd> InschrijvingWedstrijden { get; }
        IGenericRepository<WedstrijdReeks> WedstrijdReeksen { get; }
        IGenericRepository<Carpool> Carpools { get; }
        IGenericRepository<Mail> Mails { get; }
        IGenericRepository<VoogdZwemmer> VoogdZwemmers { get; }
        IGenericRepository<Plaats> Plaatsen { get; }
        IGenericRepository<Geslacht> Geslachten { get; }
        IGenericRepository<Wedstrijd> Wedstrijden { get; }
        Task SaveChangesAsync();
    }
}

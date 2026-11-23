namespace Zwemclub.Data.UnitOfWork
{
    public class UnitOfWork(ZwemclubContext context) : IUnitOfWork
    {
        private IGenericRepository<CustomUser>? gebruikers;
        private IGenericRepository<GebruikerCarpool>? gebruikerCarpools;
        private IGenericRepository<Parameter>? parameters;
        private IGenericRepository<TrainerGroep>? trainerGroepen;
        private IGenericRepository<ReeksType>? reeksTypes;
        private IGenericRepository<InschrijvingWedstrijd>? inschrijvingWedstrijden;
        private IGenericRepository<WedstrijdReeks>? wedstrijdReeksen;
        private IGenericRepository<Carpool>? carpools;
        private IGenericRepository<Mail>? mails;
        private IGenericRepository<VoogdZwemmer>? voogdZwemmers;
        private IGenericRepository<Plaats>? plaatsen;
        private IGenericRepository<Geslacht>? geslachten;
        private IGenericRepository<Wedstrijd>? wedstrijden;

        public IGenericRepository<CustomUser> Gebruikers =>
            gebruikers ??= new GenericRepository<CustomUser>(context);

        public IGenericRepository<GebruikerCarpool> GebruikerCarpools =>
            gebruikerCarpools ??= new GenericRepository<GebruikerCarpool>(context);

        public IGenericRepository<Parameter> Parameters =>
            parameters ??= new GenericRepository<Parameter>(context);

        public IGenericRepository<TrainerGroep> TrainerGroepen =>
            trainerGroepen ??= new GenericRepository<TrainerGroep>(context);

        public IGenericRepository<ReeksType> ReeksTypes =>
            reeksTypes ??= new GenericRepository<ReeksType>(context);

        public IGenericRepository<InschrijvingWedstrijd> InschrijvingWedstrijden =>
            inschrijvingWedstrijden ??= new GenericRepository<InschrijvingWedstrijd>(context);

        public IGenericRepository<WedstrijdReeks> WedstrijdReeksen =>
            wedstrijdReeksen ??= new GenericRepository<WedstrijdReeks>(context);

        public IGenericRepository<Carpool> Carpools =>
            carpools ??= new GenericRepository<Carpool>(context);

        public IGenericRepository<Mail> Mails =>
            mails ??= new GenericRepository<Mail>(context);

        public IGenericRepository<VoogdZwemmer> VoogdZwemmers =>
            voogdZwemmers ??= new GenericRepository<VoogdZwemmer>(context);

        public IGenericRepository<Plaats> Plaatsen =>
            plaatsen ??= new GenericRepository<Plaats>(context);

        public IGenericRepository<Geslacht> Geslachten =>
            geslachten ??= new GenericRepository<Geslacht>(context);

        public IGenericRepository<Wedstrijd> Wedstrijden =>
            wedstrijden ??= new GenericRepository<Wedstrijd>(context);

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

namespace Zwemclub.Data.UnitOfWork
{
    public class UnitOfWork(ZwemclubContext context) : IUnitOfWork
    {
        private IGebruikerRepository? gebruikers;
        private ITrainerGroepRepository? trainerGroepen;
        private IInschrijvingWedstrijdRepository? inschrijvingWedstrijden;
        private IWedstrijdReeksRepository? wedstrijdReeksen;
        private IVoogdZwemmerRepository? voogdZwemmers;
        private IWedstrijdRepository? wedstrijden;

        private IGenericRepository<Parameter>? parameters;
        private IGenericRepository<ReeksType>? reeksTypes;
        private IGenericRepository<Mail>? mails;
        private IGenericRepository<Plaats>? plaatsen;
        private IGenericRepository<Geslacht>? geslachten;
        private IGenericRepository<ContactBericht>? contactBerichten;

        public IGebruikerRepository Gebruikers =>
            gebruikers ??= new GebruikerRepository(context);

        public ITrainerGroepRepository TrainerGroepen =>
            trainerGroepen ??= new TrainerGroepRepository(context);

        public IInschrijvingWedstrijdRepository InschrijvingWedstrijden =>
            inschrijvingWedstrijden ??= new InschrijvingWedstrijdRepository(context);

        public IWedstrijdReeksRepository WedstrijdReeksen =>
            wedstrijdReeksen ??= new WedstrijdReeksRepository(context);

        public IVoogdZwemmerRepository VoogdZwemmers =>
            voogdZwemmers ??= new VoogdZwemmerRepository(context);

        public IWedstrijdRepository Wedstrijden =>
            wedstrijden ??= new WedstrijdRepository(context);

        public IGenericRepository<Parameter> Parameters =>
            parameters ??= new GenericRepository<Parameter>(context);

        public IGenericRepository<ReeksType> ReeksTypes =>
            reeksTypes ??= new GenericRepository<ReeksType>(context);

        public IGenericRepository<Mail> Mails =>
            mails ??= new GenericRepository<Mail>(context);

        public IGenericRepository<Plaats> Plaatsen =>
            plaatsen ??= new GenericRepository<Plaats>(context);

        public IGenericRepository<Geslacht> Geslachten =>
            geslachten ??= new GenericRepository<Geslacht>(context);

        public IGenericRepository<ContactBericht> ContactBerichten =>
            contactBerichten ??= new GenericRepository<ContactBericht>(context);

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

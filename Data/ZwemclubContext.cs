namespace Zwemclub.Data
{
    public class ZwemclubContext(DbContextOptions<ZwemclubContext> options) : IdentityDbContext<CustomUser>(options)
    {
        public DbSet<GebruikerCarpool> GebruikerCarpools => Set<GebruikerCarpool>();
        public DbSet<Parameter> Parameters => Set<Parameter>();
        public DbSet<TrainerGroep> TrainerGroepen => Set<TrainerGroep>();
        public DbSet<ReeksType> ReeksTypes => Set<ReeksType>();
        public DbSet<InschrijvingWedstrijd> InschrijvingWedstrijden => Set<InschrijvingWedstrijd>();
        public DbSet<WedstrijdReeks> WedstrijdReeksen => Set<WedstrijdReeks>();
        public DbSet<Carpool> Carpools => Set<Carpool>();
        public DbSet<Mail> Mails => Set<Mail>();
        public DbSet<VoogdZwemmer> VoogdZwemmers => Set<VoogdZwemmer>();
        public DbSet<Plaats> Plaatsen => Set<Plaats>();
        public DbSet<Geslacht> Geslachten => Set<Geslacht>();
        public DbSet<Wedstrijd> Wedstrijden => Set<Wedstrijd>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<GebruikerCarpool>().ToTable("GebruikerCarpool");
            modelBuilder.Entity<Parameter>().ToTable("Parameter");
            modelBuilder.Entity<TrainerGroep>().ToTable("TrainerGroep");
            modelBuilder.Entity<ReeksType>().ToTable("ReeksType");
            modelBuilder.Entity<WedstrijdReeks>().HasData(
                new WedstrijdReeks
                {
                    Id = 2,
                    GeslachtId = 2,
                    ReeksTypeId = 1,
                    WedstrijdId = 1,
                    Startuur = "10:15",
                    MaxAantalInschrijvingen = 24
                },
                new WedstrijdReeks
                {
                    Id = 3,
                    GeslachtId = 3,
                    ReeksTypeId = 1,
                    WedstrijdId = 1,
                    Startuur = "10:30",
                    MaxAantalInschrijvingen = 24
                },
                new WedstrijdReeks
                {
                    Id = 4,
                    GeslachtId = 1,
                    ReeksTypeId = 2,
                    WedstrijdId = 1,
                    Startuur = "11:00",
                    MaxAantalInschrijvingen = 24
                },
                new WedstrijdReeks
                {
                    Id = 5,
                    GeslachtId = 2,
                    ReeksTypeId = 2,
                    WedstrijdId = 1,
                    Startuur = "11:15",
                    MaxAantalInschrijvingen = 24
                },
                new WedstrijdReeks
                {
                    Id = 6,
                    GeslachtId = 3,
                    ReeksTypeId = 2,
                    WedstrijdId = 1,
                    Startuur = "11:30",
                    MaxAantalInschrijvingen = 24
                });

            modelBuilder.Entity<InschrijvingWedstrijd>().ToTable("InschrijvingWedstrijd");
            modelBuilder.Entity<WedstrijdReeks>().ToTable("WedstrijdReeks");
            modelBuilder.Entity<Carpool>().ToTable("Carpool");
            modelBuilder.Entity<Mail>().ToTable("Mail");
            modelBuilder.Entity<VoogdZwemmer>().ToTable("VoogdZwemmer");
            modelBuilder.Entity<Plaats>().ToTable("Plaats");
            modelBuilder.Entity<Geslacht>().ToTable("Geslacht");
            modelBuilder.Entity<Wedstrijd>().ToTable("Wedstrijd");

            modelBuilder.Entity<CustomUser>()
                .HasOne(e => e.Geslacht)
                .WithMany(e => e.Gebruikers)
                .HasForeignKey(e => e.GeslachtId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<GebruikerCarpool>()
                .HasOne(e => e.Gebruiker)
                .WithMany(e => e.CarpoolDeelnames)
                .HasForeignKey(e => e.GebruikerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GebruikerCarpool>()
                .HasOne(e => e.Carpool)
                .WithMany(e => e.Deelnemers)
                .HasForeignKey(e => e.CarpoolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Carpool>()
                .HasOne(e => e.Ouder)
                .WithMany(e => e.AangemaakteCarpools)
                .HasForeignKey(e => e.OuderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carpool>()
                .HasOne(e => e.Wedstrijd)
                .WithMany(e => e.Carpools)
                .HasForeignKey(e => e.WedstrijdId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoogdZwemmer>()
                .HasOne(e => e.Voogd)
                .WithMany(e => e.ZwemmersAlsVoogd)
                .HasForeignKey(e => e.VoogdId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<VoogdZwemmer>()
                .HasOne(e => e.Zwemmer)
                .WithMany(e => e.VoogdenAlsZwemmer)
                .HasForeignKey(e => e.ZwemmerId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<TrainerGroep>()
                .HasOne(e => e.Trainer)
                .WithMany(e => e.ZwemmersAlsTrainer)
                .HasForeignKey(e => e.TrainerId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<TrainerGroep>()
                .HasOne(e => e.Zwemmer)
                .WithMany(e => e.TrainersAlsZwemmer)
                .HasForeignKey(e => e.ZwemmerId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<InschrijvingWedstrijd>()
                .HasOne(e => e.Zwemmer)
                .WithMany(e => e.WedstrijdInschrijvingen)
                .HasForeignKey(e => e.ZwemmerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InschrijvingWedstrijd>()
                .HasOne(e => e.WedstrijdReeks)
                .WithMany(e => e.Inschrijvingen)
                .HasForeignKey(e => e.WedstrijdReeksId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WedstrijdReeks>()
                .HasOne(e => e.Geslacht)
                .WithMany(e => e.WedstrijdReeksen)
                .HasForeignKey(e => e.GeslachtId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WedstrijdReeks>()
                .HasOne(e => e.ReeksType)
                .WithMany(e => e.WedstrijdReeksen)
                .HasForeignKey(e => e.ReeksTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WedstrijdReeks>()
                .HasOne(e => e.Wedstrijd)
                .WithMany(e => e.Reeksen)
                .HasForeignKey(e => e.WedstrijdId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wedstrijd>()
                .HasOne(e => e.Plaats)
                .WithMany(e => e.Wedstrijden)
                .HasForeignKey(e => e.PlaatsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomUser>().HasIndex(e => e.UniekNummer).IsUnique();
        
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "rol-voorzitter", Name = "Voorzitter", NormalizedName = "VOORZITTER", ConcurrencyStamp = "rol-voorzitter-v1" },
                new IdentityRole { Id = "rol-trainer", Name = "Trainer", NormalizedName = "TRAINER", ConcurrencyStamp = "rol-trainer-v1" },
                new IdentityRole { Id = "rol-ouder", Name = "Ouder", NormalizedName = "OUDER", ConcurrencyStamp = "rol-ouder-v1" },
                new IdentityRole { Id = "rol-zwemmer", Name = "Zwemmer", NormalizedName = "ZWEMMER", ConcurrencyStamp = "rol-zwemmer-v1" });

            modelBuilder.Entity<Geslacht>().HasData(
                new Geslacht { Id = 1, Naam = "Man" },
                new Geslacht { Id = 2, Naam = "Vrouw" },
                new Geslacht { Id = 3, Naam = "Andere" });

            // Vaste stamps voorkomen modelwijzigingen bij iedere nieuwe migratie.
            // Deze voorbeeldprofielen hebben bewust geen wachtwoord of werkende login.
            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser
                {
                    Id = "zwemclub-ouder",
                    UserName = "ouder@zwemclub.example",
                    NormalizedUserName = "OUDER@ZWEMCLUB.EXAMPLE",
                    Email = "ouder@zwemclub.example",
                    NormalizedEmail = "OUDER@ZWEMCLUB.EXAMPLE",
                    Naam = "Peeters",
                    Voornaam = "Sofie",
                    Geboortedatum = new DateOnly(1985, 3, 12),
                    GeslachtId = 2,
                    UniekNummer = "ZC-0001",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "ouder-security-v1",
                    ConcurrencyStamp = "ouder-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer",
                    UserName = "zwemmer@zwemclub.example",
                    NormalizedUserName = "ZWEMMER@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer@zwemclub.example",
                    NormalizedEmail = "ZWEMMER@ZWEMCLUB.EXAMPLE",
                    Naam = "Peeters",
                    Voornaam = "Noah",
                    Geboortedatum = new DateOnly(2012, 6, 20),
                    GeslachtId = 1,
                    UniekNummer = "ZC-0002",
                    FederatieNummer = "VZF-0002",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-security-v1",
                    ConcurrencyStamp = "zwemmer-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-trainer",
                    UserName = "trainer@zwemclub.example",
                    NormalizedUserName = "TRAINER@ZWEMCLUB.EXAMPLE",
                    Email = "trainer@zwemclub.example",
                    NormalizedEmail = "TRAINER@ZWEMCLUB.EXAMPLE",
                    Naam = "Janssens",
                    Voornaam = "Bram",
                    GeslachtId = 1,
                    UniekNummer = "ZC-0003",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "trainer-security-v1",
                    ConcurrencyStamp = "trainer-concurrency-v1"
                });

            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser
                {
                    Id = "zwemclub-voorzitter",
                    UserName = "voorzitter@zwemclub.example",
                    NormalizedUserName = "VOORZITTER@ZWEMCLUB.EXAMPLE",
                    Email = "voorzitter@zwemclub.example",
                    NormalizedEmail = "VOORZITTER@ZWEMCLUB.EXAMPLE",
                    Naam = "Vermeulen",
                    Voornaam = "An",
                    GeslachtId = 2,
                    UniekNummer = "ZC-0004",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "voorzitter-security-v1",
                    ConcurrencyStamp = "voorzitter-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer-2",
                    UserName = "zwemmer-2@zwemclub.example",
                    NormalizedUserName = "ZWEMMER-2@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer-2@zwemclub.example",
                    NormalizedEmail = "ZWEMMER-2@ZWEMCLUB.EXAMPLE",
                    Naam = "De Smet",
                    Voornaam = "Lars",
                    Geboortedatum = new DateOnly(2011, 2, 14),
                    GeslachtId = 1,
                    UniekNummer = "ZC-0005",
                    FederatieNummer = "VZF-0005",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-2-security-v1",
                    ConcurrencyStamp = "zwemmer-2-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer-3",
                    UserName = "zwemmer-3@zwemclub.example",
                    NormalizedUserName = "ZWEMMER-3@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer-3@zwemclub.example",
                    NormalizedEmail = "ZWEMMER-3@ZWEMCLUB.EXAMPLE",
                    Naam = "Willems",
                    Voornaam = "Emma",
                    Geboortedatum = new DateOnly(2012, 7, 9),
                    GeslachtId = 2,
                    UniekNummer = "ZC-0006",
                    FederatieNummer = "VZF-0006",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-3-security-v1",
                    ConcurrencyStamp = "zwemmer-3-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer-4",
                    UserName = "zwemmer-4@zwemclub.example",
                    NormalizedUserName = "ZWEMMER-4@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer-4@zwemclub.example",
                    NormalizedEmail = "ZWEMMER-4@ZWEMCLUB.EXAMPLE",
                    Naam = "Maes",
                    Voornaam = "Alex",
                    Geboortedatum = new DateOnly(2011, 11, 3),
                    GeslachtId = 3,
                    UniekNummer = "ZC-0007",
                    FederatieNummer = "VZF-0007",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-4-security-v1",
                    ConcurrencyStamp = "zwemmer-4-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer-5",
                    UserName = "zwemmer-5@zwemclub.example",
                    NormalizedUserName = "ZWEMMER-5@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer-5@zwemclub.example",
                    NormalizedEmail = "ZWEMMER-5@ZWEMCLUB.EXAMPLE",
                    Naam = "Jacobs",
                    Voornaam = "Milan",
                    Geboortedatum = new DateOnly(2013, 1, 18),
                    GeslachtId = 1,
                    UniekNummer = "ZC-0008",
                    FederatieNummer = "VZF-0008",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-5-security-v1",
                    ConcurrencyStamp = "zwemmer-5-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-zwemmer-6",
                    UserName = "zwemmer-6@zwemclub.example",
                    NormalizedUserName = "ZWEMMER-6@ZWEMCLUB.EXAMPLE",
                    Email = "zwemmer-6@zwemclub.example",
                    NormalizedEmail = "ZWEMMER-6@ZWEMCLUB.EXAMPLE",
                    Naam = "Claes",
                    Voornaam = "Louise",
                    Geboortedatum = new DateOnly(2012, 9, 22),
                    GeslachtId = 2,
                    UniekNummer = "ZC-0009",
                    FederatieNummer = "VZF-0009",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "zwemmer-6-security-v1",
                    ConcurrencyStamp = "zwemmer-6-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-ouder-2",
                    UserName = "ouder-2@zwemclub.example",
                    NormalizedUserName = "OUDER-2@ZWEMCLUB.EXAMPLE",
                    Email = "ouder-2@zwemclub.example",
                    NormalizedEmail = "OUDER-2@ZWEMCLUB.EXAMPLE",
                    Naam = "De Smet",
                    Voornaam = "Koen",
                    Geboortedatum = new DateOnly(1982, 5, 10),
                    GeslachtId = 1,
                    UniekNummer = "ZC-0010",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "ouder-2-security-v1",
                    ConcurrencyStamp = "ouder-2-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-ouder-3",
                    UserName = "ouder-3@zwemclub.example",
                    NormalizedUserName = "OUDER-3@ZWEMCLUB.EXAMPLE",
                    Email = "ouder-3@zwemclub.example",
                    NormalizedEmail = "OUDER-3@ZWEMCLUB.EXAMPLE",
                    Naam = "Willems",
                    Voornaam = "Sara",
                    Geboortedatum = new DateOnly(1984, 8, 16),
                    GeslachtId = 2,
                    UniekNummer = "ZC-0011",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "ouder-3-security-v1",
                    ConcurrencyStamp = "ouder-3-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-ouder-4",
                    UserName = "ouder-4@zwemclub.example",
                    NormalizedUserName = "OUDER-4@ZWEMCLUB.EXAMPLE",
                    Email = "ouder-4@zwemclub.example",
                    NormalizedEmail = "OUDER-4@ZWEMCLUB.EXAMPLE",
                    Naam = "Maes",
                    Voornaam = "Robin",
                    Geboortedatum = new DateOnly(1983, 12, 7),
                    GeslachtId = 3,
                    UniekNummer = "ZC-0012",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "ouder-4-security-v1",
                    ConcurrencyStamp = "ouder-4-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-trainer-2",
                    UserName = "trainer-2@zwemclub.example",
                    NormalizedUserName = "TRAINER-2@ZWEMCLUB.EXAMPLE",
                    Email = "trainer-2@zwemclub.example",
                    NormalizedEmail = "TRAINER-2@ZWEMCLUB.EXAMPLE",
                    Naam = "Van den Broeck",
                    Voornaam = "Julie",
                    Geboortedatum = new DateOnly(1990, 3, 25),
                    GeslachtId = 2,
                    UniekNummer = "ZC-0013",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "trainer-2-security-v1",
                    ConcurrencyStamp = "trainer-2-concurrency-v1"
                },
                new CustomUser
                {
                    Id = "zwemclub-trainer-3",
                    UserName = "trainer-3@zwemclub.example",
                    NormalizedUserName = "TRAINER-3@ZWEMCLUB.EXAMPLE",
                    Email = "trainer-3@zwemclub.example",
                    NormalizedEmail = "TRAINER-3@ZWEMCLUB.EXAMPLE",
                    Naam = "Verstraeten",
                    Voornaam = "Tom",
                    Geboortedatum = new DateOnly(1988, 10, 12),
                    GeslachtId = 1,
                    UniekNummer = "ZC-0014",
                    IsActief = true,
                    IsVerwijderd = false,
                    SecurityStamp = "trainer-3-security-v1",
                    ConcurrencyStamp = "trainer-3-concurrency-v1"
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "zwemclub-ouder", RoleId = "rol-ouder" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-trainer", RoleId = "rol-trainer" },
                new IdentityUserRole<string> { UserId = "zwemclub-voorzitter", RoleId = "rol-voorzitter" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer-2", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer-3", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer-4", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer-5", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-zwemmer-6", RoleId = "rol-zwemmer" },
                new IdentityUserRole<string> { UserId = "zwemclub-ouder-2", RoleId = "rol-ouder" },
                new IdentityUserRole<string> { UserId = "zwemclub-ouder-3", RoleId = "rol-ouder" },
                new IdentityUserRole<string> { UserId = "zwemclub-ouder-4", RoleId = "rol-ouder" },
                new IdentityUserRole<string> { UserId = "zwemclub-trainer-2", RoleId = "rol-trainer" },
                new IdentityUserRole<string> { UserId = "zwemclub-trainer-3", RoleId = "rol-trainer" });

            modelBuilder.Entity<Plaats>().HasData(new Plaats
            {
                Id = 1,
                Naam = "Sportoase Leuven",
                Straat = "Philipssite",
                HuisNummer = "6",
                Postcode = "3001",
                Gemeente = "Leuven",
                Land = "België"
            });

            modelBuilder.Entity<Wedstrijd>().HasData(new Wedstrijd
            {
                Id = 1,
                Naam = "Zwemclub herfstmeeting",
                Datum = new DateOnly(2026, 11, 15),
                DeadlineInschrijven = new DateTime(2026, 11, 8, 20, 0, 0),
                PlaatsId = 1
            });

            modelBuilder.Entity<ReeksType>().HasData(
                new ReeksType { Id = 1, Naam = "50 meter vrije slag" },
                new ReeksType { Id = 2, Naam = "100 meter schoolslag" });

            modelBuilder.Entity<WedstrijdReeks>().HasData(new WedstrijdReeks
            {
                Id = 1,
                GeslachtId = 1,
                ReeksTypeId = 1,
                WedstrijdId = 1,
                Startuur = "10:00",
                MaxAantalInschrijvingen = 24
            });

            modelBuilder.Entity<InschrijvingWedstrijd>().HasData(new InschrijvingWedstrijd
            {
                Id = 1,
                ZwemmerId = "zwemclub-zwemmer",
                WedstrijdReeksId = 1,
                DatumInschrijving = new DateTime(2026, 10, 20, 18, 0, 0)
            });

            modelBuilder.Entity<InschrijvingWedstrijd>().HasData(
                new InschrijvingWedstrijd
                {
                    Id = 2,
                    ZwemmerId = "zwemclub-zwemmer",
                    WedstrijdReeksId = 4,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 3,
                    ZwemmerId = "zwemclub-zwemmer-2",
                    WedstrijdReeksId = 1,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 4,
                    ZwemmerId = "zwemclub-zwemmer-2",
                    WedstrijdReeksId = 4,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 5,
                    ZwemmerId = "zwemclub-zwemmer-3",
                    WedstrijdReeksId = 2,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 6,
                    ZwemmerId = "zwemclub-zwemmer-3",
                    WedstrijdReeksId = 5,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 7,
                    ZwemmerId = "zwemclub-zwemmer-4",
                    WedstrijdReeksId = 3,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 8,
                    ZwemmerId = "zwemclub-zwemmer-4",
                    WedstrijdReeksId = 6,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 9,
                    ZwemmerId = "zwemclub-zwemmer-5",
                    WedstrijdReeksId = 1,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 10,
                    ZwemmerId = "zwemclub-zwemmer-5",
                    WedstrijdReeksId = 4,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 11,
                    ZwemmerId = "zwemclub-zwemmer-6",
                    WedstrijdReeksId = 2,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                },
                new InschrijvingWedstrijd
                {
                    Id = 12,
                    ZwemmerId = "zwemclub-zwemmer-6",
                    WedstrijdReeksId = 5,
                    DatumInschrijving = new DateTime(2026, 10, 23, 18, 0, 0),
                    DatumDefinitief = new DateTime(2026, 10, 24, 12, 0, 0)
                });

            modelBuilder.Entity<Carpool>().HasData(new Carpool
            {
                Id = 1,
                OuderId = "zwemclub-ouder",
                WedstrijdId = 1,
                AantalBeschikbarePlaatsen = 3,
                VertrekPlaats = "Parking clubzwembad",
                VertrekUur = new DateTime(2026, 11, 15, 8, 30, 0),
                GeschatUurTerugkomst = new DateTime(2026, 11, 15, 15, 0, 0),
                IsVerwijderd = false
            });

            modelBuilder.Entity<GebruikerCarpool>().HasData(new GebruikerCarpool
            {
                Id = 1,
                GebruikerId = "zwemclub-zwemmer",
                CarpoolId = 1,
                IsBevestigd = true,
                DatumAanvraag = new DateOnly(2026, 10, 21),
                DatumBevestigd = new DateOnly(2026, 10, 22),
                AantalPersonen = 1
            });

            modelBuilder.Entity<VoogdZwemmer>().HasData(new VoogdZwemmer
            {
                Id = 1,
                VoogdId = "zwemclub-ouder",
                ZwemmerId = "zwemclub-zwemmer",
                IsMailGestuurd = false
            });

            modelBuilder.Entity<TrainerGroep>().HasData(new TrainerGroep
            {
                Id = 1,
                TrainerId = "zwemclub-trainer",
                ZwemmerId = "zwemclub-zwemmer",
                DatumVanaf = new DateOnly(2026, 9, 1),
                DatumTot = new DateOnly(2027, 8, 31)
            });

            modelBuilder.Entity<VoogdZwemmer>().HasData(
                new VoogdZwemmer
                {
                    Id = 2,
                    VoogdId = "zwemclub-ouder-2",
                    ZwemmerId = "zwemclub-zwemmer-2",
                    IsMailGestuurd = false
                },
                new VoogdZwemmer
                {
                    Id = 3,
                    VoogdId = "zwemclub-ouder-3",
                    ZwemmerId = "zwemclub-zwemmer-3",
                    IsMailGestuurd = false
                },
                new VoogdZwemmer
                {
                    Id = 4,
                    VoogdId = "zwemclub-ouder-4",
                    ZwemmerId = "zwemclub-zwemmer-4",
                    IsMailGestuurd = false
                },
                new VoogdZwemmer
                {
                    Id = 5,
                    VoogdId = "zwemclub-ouder-2",
                    ZwemmerId = "zwemclub-zwemmer-5",
                    IsMailGestuurd = false
                },
                new VoogdZwemmer
                {
                    Id = 6,
                    VoogdId = "zwemclub-ouder-3",
                    ZwemmerId = "zwemclub-zwemmer-6",
                    IsMailGestuurd = false
                });

            modelBuilder.Entity<TrainerGroep>().HasData(
                new TrainerGroep
                {
                    Id = 2,
                    TrainerId = "zwemclub-trainer-2",
                    ZwemmerId = "zwemclub-zwemmer-2",
                    DatumVanaf = new DateOnly(2026, 9, 1),
                    DatumTot = new DateOnly(2027, 8, 31)
                },
                new TrainerGroep
                {
                    Id = 3,
                    TrainerId = "zwemclub-trainer-2",
                    ZwemmerId = "zwemclub-zwemmer-3",
                    DatumVanaf = new DateOnly(2026, 9, 1),
                    DatumTot = new DateOnly(2027, 8, 31)
                },
                new TrainerGroep
                {
                    Id = 4,
                    TrainerId = "zwemclub-trainer-3",
                    ZwemmerId = "zwemclub-zwemmer-4",
                    DatumVanaf = new DateOnly(2026, 9, 1),
                    DatumTot = new DateOnly(2027, 8, 31)
                },
                new TrainerGroep
                {
                    Id = 5,
                    TrainerId = "zwemclub-trainer-3",
                    ZwemmerId = "zwemclub-zwemmer-5",
                    DatumVanaf = new DateOnly(2026, 9, 1),
                    DatumTot = new DateOnly(2027, 8, 31)
                },
                new TrainerGroep
                {
                    Id = 6,
                    TrainerId = "zwemclub-trainer-2",
                    ZwemmerId = "zwemclub-zwemmer-6",
                    DatumVanaf = new DateOnly(2026, 9, 1),
                    DatumTot = new DateOnly(2027, 8, 31)
                });

            modelBuilder.Entity<Parameter>().HasData(new Parameter { Id = 1, Naam = "Clubnaam", Waarde = "Zwemclub" });
            modelBuilder.Entity<Mail>().HasData(new Mail
            {
                Id = 1,
                Naam = "Inschrijving bevestigd",
                Onderwerp = "Je inschrijving bij Zwemclub",
                Body = "Je inschrijving voor de wedstrijd is bevestigd."
            });

        }
    }
}

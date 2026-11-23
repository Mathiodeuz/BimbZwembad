namespace Zwemclub.Models
{
    public class CustomUser : IdentityUser
    {
        [MaxLength(256)]
        public string? Naam { get; set; }

        [MaxLength(256)]
        public string? Voornaam { get; set; }

        public DateOnly? Geboortedatum { get; set; }

        public int? GeslachtId { get; set; }

        [Required]
        [MaxLength(256)]
        public string UniekNummer { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? FederatieNummer { get; set; }

        [MaxLength(2000)]
        public string? ProfielFoto { get; set; }

        public bool IsActief { get; set; }

        public bool? IsVerwijderd { get; set; }

        public Geslacht? Geslacht { get; set; }

        public ICollection<GebruikerCarpool> CarpoolDeelnames { get; set; } = [];

        public ICollection<Carpool> AangemaakteCarpools { get; set; } = [];

        public ICollection<VoogdZwemmer> ZwemmersAlsVoogd { get; set; } = [];

        public ICollection<VoogdZwemmer> VoogdenAlsZwemmer { get; set; } = [];

        public ICollection<TrainerGroep> ZwemmersAlsTrainer { get; set; } = [];

        public ICollection<TrainerGroep> TrainersAlsZwemmer { get; set; } = [];

        public ICollection<InschrijvingWedstrijd> WedstrijdInschrijvingen { get; set; } = [];
    }
}

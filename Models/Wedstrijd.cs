namespace Zwemclub.Models
{
    public class Wedstrijd
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        public DateOnly Datum { get; set; }

        public DateTime DeadlineInschrijven { get; set; }

        [MaxLength(2000)]
        public string? UrlWedstrijd { get; set; }

        public int PlaatsId { get; set; }

        public bool IsVerwijderd { get; set; }

        public ICollection<Carpool> Carpools { get; set; } = [];

        public ICollection<WedstrijdReeks> Reeksen { get; set; } = [];

        public Plaats Plaats { get; set; } = null!;
    }
}

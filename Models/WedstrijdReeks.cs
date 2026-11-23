namespace Zwemclub.Models
{
    public class WedstrijdReeks
    {
        public int Id { get; set; }

        public int GeslachtId { get; set; }

        public int ReeksTypeId { get; set; }

        public int WedstrijdId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Startuur { get; set; } = string.Empty;

        public int MaxAantalInschrijvingen { get; set; }

        public bool IsVerwijderd { get; set; }

        public ICollection<InschrijvingWedstrijd> Inschrijvingen { get; set; } = [];

        public Geslacht Geslacht { get; set; } = null!;

        public ReeksType ReeksType { get; set; } = null!;

        public Wedstrijd Wedstrijd { get; set; } = null!;
    }
}

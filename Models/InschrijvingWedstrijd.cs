namespace Zwemclub.Models
{
    public class InschrijvingWedstrijd
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string ZwemmerId { get; set; } = string.Empty;

        public int WedstrijdReeksId { get; set; }

        public DateTime DatumInschrijving { get; set; }

        public DateTime? DatumAnnulatie { get; set; }

        [MaxLength(256)]
        public string? RedenAnnulatie { get; set; }

        public DateTime? DatumDefinitief { get; set; }

        [MaxLength(256)]
        public string? BehaaldeTijd { get; set; }

        [MaxLength(256)]
        public string? BehaaldePlaats { get; set; }

        public bool IsVerwijderd { get; set; }

        public CustomUser Zwemmer { get; set; } = null!;

        public WedstrijdReeks WedstrijdReeks { get; set; } = null!;
    }
}

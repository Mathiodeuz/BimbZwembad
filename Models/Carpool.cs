namespace Zwemclub.Models
{
    public class Carpool
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string OuderId { get; set; } = string.Empty;

        public int WedstrijdId { get; set; }

        public int AantalBeschikbarePlaatsen { get; set; }

        [Required]
        [MaxLength(256)]
        public string VertrekPlaats { get; set; } = string.Empty;

        public DateTime VertrekUur { get; set; }

        public DateTime? GeschatUurTerugkomst { get; set; }

        public bool? IsVerwijderd { get; set; }

        public ICollection<GebruikerCarpool> Deelnemers { get; set; } = [];

        public CustomUser Ouder { get; set; } = null!;

        public Wedstrijd Wedstrijd { get; set; } = null!;
    }
}

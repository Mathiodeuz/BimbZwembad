namespace Zwemclub.Models
{
    public class GebruikerCarpool
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string GebruikerId { get; set; } = string.Empty;

        public int CarpoolId { get; set; }

        public bool IsBevestigd { get; set; }

        public DateOnly DatumAanvraag { get; set; }

        public DateOnly DatumBevestigd { get; set; }

        public int AantalPersonen { get; set; }

        public CustomUser Gebruiker { get; set; } = null!;

        public Carpool Carpool { get; set; } = null!;
    }
}

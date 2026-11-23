namespace Zwemclub.Models
{
    public class Plaats
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Straat { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string HuisNummer { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Postcode { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Gemeente { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? Land { get; set; }

        public bool IsVerwijderd { get; set; }

        public ICollection<Wedstrijd> Wedstrijden { get; set; } = [];
    }
}

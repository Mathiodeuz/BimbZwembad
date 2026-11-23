namespace Zwemclub.Models
{
    public class Geslacht
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        public bool IsVerwijderd { get; set; }

        public ICollection<CustomUser> Gebruikers { get; set; } = [];

        public ICollection<WedstrijdReeks> WedstrijdReeksen { get; set; } = [];
    }
}

namespace Zwemclub.Models
{
    public class ReeksType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        public bool IsVerwijderd { get; set; }

        public ICollection<WedstrijdReeks> WedstrijdReeksen { get; set; } = [];
    }
}

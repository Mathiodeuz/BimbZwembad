namespace Zwemclub.Models
{
    public class Mail
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Onderwerp { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Body { get; set; } = string.Empty;

        public bool IsVerwijderd { get; set; }
    }
}

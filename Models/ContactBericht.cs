using System.ComponentModel.DataAnnotations;

namespace Zwemclub.Models
{
    public class ContactBericht
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naam { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Onderwerp { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Bericht { get; set; } = string.Empty;

        public DateTime VerzondenOp { get; set; } = DateTime.UtcNow;

        public string? AfzenderIp { get; set; }

        public bool IsBeantwoord { get; set; } = false;
    }
}
using System.ComponentModel.DataAnnotations;

namespace Zwemclub.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Vul je naam in.")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 tekens bevatten.")]
        [Display(Name = "Naam")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vul een geldig e-mailadres in.")]
        [EmailAddress(ErrorMessage = "Vul een geldig e-mailadres in.")]
        [StringLength(150, ErrorMessage = "E-mailadres mag maximaal 150 tekens bevatten.")]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kies of vul een onderwerp in.")]
        [StringLength(150, ErrorMessage = "Onderwerp mag maximaal 150 tekens bevatten.")]
        [Display(Name = "Onderwerp")]
        public string Onderwerp { get; set; } = string.Empty;

        [Required(ErrorMessage = "Typ je bericht.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Het bericht moet tussen 10 en 2000 tekens lang zijn.")]
        [Display(Name = "Bericht")]
        public string Bericht { get; set; } = string.Empty;

        public string? Website { get; set; }
    }
}
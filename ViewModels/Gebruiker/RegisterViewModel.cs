using System.ComponentModel.DataAnnotations;
using Zwemclub.ViewModels;
using Zwemclub.ViewModels.Gebruiker;


namespace Zwemclub.ViewModels.Gebruiker
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Familienaam is verplicht.")]
        [Display(Name = "Familienaam")]
        public string Achternaam { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "Geef een geldig e-mailadres op.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        [StringLength(
            100,
            MinimumLength = 8,
            ErrorMessage = "Het wachtwoord moet minstens 8 tekens bevatten."
        )]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bevestig je wachtwoord.")]
        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare(
            nameof(Password),
            ErrorMessage = "De wachtwoorden komen niet overeen."
        )]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Geboortedatum is verplicht.")]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateOnly Geboortedatum { get; set; }

        [Display(Name = "Federatienummer")]
        public string? FederatieNummer { get; set; }

        [Display(Name = "Geslacht")]
        public int? GeslachtId { get; set; }
    }
}
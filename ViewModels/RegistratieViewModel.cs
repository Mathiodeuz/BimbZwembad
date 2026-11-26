namespace Zwemclub.ViewModels
{
    public class RegistratieViewModel
    {
        [Required]
        public string Rol { get; set; } = string.Empty;

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [MaxLength(256)]
        public string Voornaam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Ongeldig telefoonnummer.")]
        public string? Telefoon { get; set; }

        public DateOnly? Geboortedatum { get; set; }
        [Required(ErrorMessage ="Selecteer je  geslacht.")]
        public int? GeslachtId { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; } = string.Empty;

        [Compare(nameof(Wachtwoord), ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [DataType(DataType.Password)]
        public string BevestigWachtwoord { get; set; } = string.Empty;
    }
}

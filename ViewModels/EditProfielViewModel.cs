namespace Zwemclub.ViewModels
{
    public class EditProfielViewModel
    {
        [Required]
        [MaxLength(256)]
        public string Voornaam { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Naam { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Ongeldig telefoonnummer.")]
        public string? Telefoon { get; set; }
        public DateOnly? Geboortedatum { get; set; }
        public string? GeslachtsNaam { get; set; }
        public string? FederatieNummer { get; set; } = string.Empty;
        public string UniekNummer { get; set; } = string.Empty;
    }
}

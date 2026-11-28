namespace Zwemclub.ViewModels
{
    public class WachtwoordResettenViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nieuw wachtwoord is verplicht.")]
        [MinLength(8, ErrorMessage = "Het wachtwoord moet minstens 8 tekens bevatten.")]
        [DataType(DataType.Password)]
        public string NieuwWachtwoord { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bevestig je wachtwoord.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NieuwWachtwoord), ErrorMessage = "De wachtwoorden komen niet overeen.")]
        public string BevestigWachtwoord { get; set; } = string.Empty;
    }
}

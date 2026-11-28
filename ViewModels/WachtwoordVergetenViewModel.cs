namespace Zwemclub.ViewModels
{
    public class WachtwoordVergetenViewModel
    {
        [Required(ErrorMessage ="E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage ="Geef een geldig e-mailadres in.")]
        public string Email { get; set; } = string.Empty;
    }
}

namespace Zwemclub.ViewModels
{
    public class ProfielViewModel
    {
        
        public string Voornaam { get; set; } = string.Empty;
        public string Naam { get; set; } = string.Empty;
        public string? ProfielFoto { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Telefoon { get; set; }
        public DateOnly? Geboortedatum { get; set; }
        public string? GeslachtsNaam { get; set; }
        public string? FederatieNummer { get; set; }
        public string UniekNummer { get; set; } = string.Empty;
    }
}

namespace Zwemclub.Models
{
    public class VoogdZwemmer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string VoogdId { get; set; } = string.Empty;

        [Required]
        [MaxLength(450)]
        public string ZwemmerId { get; set; } = string.Empty;

        public bool? IsMailGestuurd { get; set; }

        public CustomUser Voogd { get; set; } = null!;

        public CustomUser Zwemmer { get; set; } = null!;
    }
}

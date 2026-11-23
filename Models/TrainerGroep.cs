namespace Zwemclub.Models
{
    public class TrainerGroep
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string TrainerId { get; set; } = string.Empty;

        [Required]
        [MaxLength(450)]
        public string ZwemmerId { get; set; } = string.Empty;

        public DateOnly DatumVanaf { get; set; }

        public DateOnly DatumTot { get; set; }

        public CustomUser Trainer { get; set; } = null!;

        public CustomUser Zwemmer { get; set; } = null!;
    }
}

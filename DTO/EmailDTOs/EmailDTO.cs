namespace Zwemclub.DTO.EmailDTOs
{
    public class EmailDTO
    {
        [Required,EmailAddress]
        public string To { get; set; } = string.Empty;
        [Required,MaxLength(256)]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
    }
}

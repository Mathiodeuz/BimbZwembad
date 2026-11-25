using Zwemclub.DTO.EmailDTOs;

namespace Zwemclub.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
    }
}

using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Zwemclub.DTO.EmailDTOs;

namespace Zwemclub.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(EmailDTO request)
        {
           
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["MailSettings:From"]!));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject= request.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("localhost",1025,MailKit.Security.SecureSocketOptions.None);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

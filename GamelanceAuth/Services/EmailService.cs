using GamelanceAuth.Models.Dto;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace GamelanceAuth.Services
{
    public class EmailService : IEmailService
    {
        public ValueTask SendEmail(EmailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(""));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();

            throw new NotImplementedException();
        }
    }
}

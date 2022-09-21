using GamelanceAuth.Models.Dto;

namespace GamelanceAuth.Services
{
    public interface IEmailService
    {
        ValueTask SendEmail(EmailRequest request);
    }
}

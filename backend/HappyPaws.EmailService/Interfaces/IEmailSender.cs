using HappyPaws.EmailService.Models;

namespace HappyPaws.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}

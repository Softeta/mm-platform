using EmailService.Send.Models.EmailService;
using System.Threading.Tasks;

namespace EmailService.Send.SendInBlue
{
    public interface ISmtpProvider
    {
        Task<string> SendEmailAsync(EmailInfo emailInfo);
    }
}

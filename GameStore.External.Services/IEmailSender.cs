using System.Threading.Tasks;

namespace GameStore.External.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

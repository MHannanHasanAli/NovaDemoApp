using System.IO;
using System.Threading.Tasks;

namespace ImperialNova
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string userEmail, string subject, string body, Stream emailStream, string fileName, string attachmentContentType);
        Task SendEmailAsync(string userEmail, string subject, string body);
    }
}

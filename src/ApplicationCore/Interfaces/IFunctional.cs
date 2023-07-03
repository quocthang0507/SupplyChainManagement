using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Interfaces
{
    public interface IFunctional
    {
        Task InitAppUserData();

        Task InitDefaultSuperAdmin();

        Task SendEmailBySendGridAsync(string apiKey,
            string fromEmail,
            string fromFullName,
            string subject,
            string message,
            string email);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(IList<IFormFile> files, IWebHostEnvironment env, string uploadFolder);
    }
}

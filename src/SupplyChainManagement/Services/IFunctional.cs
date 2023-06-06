﻿using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SupplyChainManagement.Services
{
    public interface IFunctional
    {
        Task InitAppData();

        Task CreateDefaultSuperAdmin();

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

        Task<string> UploadFile(List<IFormFile> files, IWebHostEnvironment env, string uploadFolder);
    }
}

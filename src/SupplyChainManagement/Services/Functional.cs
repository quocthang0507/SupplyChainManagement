using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SupplyChainManagement.Models;
using SupplyChainManagement.Models.Sections;
using SupplyChainManagement.Services.Database;
using System.Net;
using System.Net.Mail;

namespace SupplyChainManagement.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly UserProfilesService _userProfilesService;

        public Functional(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
            UserProfilesService userProfilesService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _userProfilesService = userProfilesService;
        }

        public async Task InitDefaultSuperAdmin()
        {
            try
            {
                ApplicationUser superAdmin = new()
                {
                    Email = _superAdminDefaultOptions.Email,
                    UserName = _superAdminDefaultOptions.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                if (result.Succeeded)
                {
                    // add to user profile
                    UserProfile profile = new();
                    profile.FirstName = _superAdminDefaultOptions.FirstName;
                    profile.LastName = _superAdminDefaultOptions.LastName;
                    profile.Email = superAdmin.Email;
                    profile.ApplicationUserId = superAdmin.Id.ToString();
                    profile.Phone = _superAdminDefaultOptions.Phone;
                    profile.Address = _superAdminDefaultOptions.Address;
                    await _userProfilesService.CreateAsync(profile);
                    await _roleManager.CreateAsync(new ApplicationRole() { Name = "Administrator" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InitAppUserData()
        {
            try
            {
                ApplicationUser user = new()
                {
                    Email = "user@example.com",
                    UserName = "user@example.com",
                    EmailConfirmed = true
                };
                UserProfile userProfile = new()
                {
                    FirstName = "User",
                    LastName = "",
                    Email = user.Email,
                    ApplicationUserId = user.Id.ToString(),
                    Address = new()
                    {
                        Commune = "Phường 8",
                        District = "Tp. Đà Lạt",
                        Province = "Lâm Đồng",
                        GeoCoordinate = new(11.963824, 108.442202)
                    }
                };

                await _userManager.CreateAsync(user, _superAdminDefaultOptions.Password);
                await _userProfilesService.CreateAsync(userProfile);
                await _roleManager.CreateAsync(new ApplicationRole() { Id = user.Id, Name = "User" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendEmailByGmailAsync(string fromEmail, string fromFullName, string subject, string messageBody, string toEmail, string toFullName, string smtpUser, string smtpPassword, string smtpHost, int smtpPort, bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);
            }
        }

        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);
        }

        public async Task<string> UploadFile(List<IFormFile> files, IWebHostEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = Path.Combine(webRoot, uploadFolder);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string? extension = Path.GetExtension(formFile.FileName);
                    string? fileName = Guid.NewGuid().ToString() + extension;
                    string? filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    result = fileName;
                }
            }
            return result;
        }
    }
}
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Seeders;
using Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly UserProfilesService _userProfilesService;
        private readonly FarmTypesService _farmTypesService;
        private readonly IRoles _roles;
        private readonly PhotoperiodismService _photoperiodismService;
        private readonly VietnamUnitsService _vietnamUnitsService;
        private readonly ProductTypesService _productTypesService;
        private readonly FarmingHouseholdsService _householdsService;
        private readonly IWebHostEnvironment _env;

        public Functional(UserManager<ApplicationUser> userManager,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
            UserProfilesService userProfilesService,
            FarmTypesService farmTypesService,
            IRoles roles,
            PhotoperiodismService photoperiodismService,
            VietnamUnitsService vietnamUnitsService,
            ProductTypesService productTypesService,
            FarmingHouseholdsService householdsService
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _userProfilesService = userProfilesService;
            _farmTypesService = farmTypesService;
            _roles = roles;
            _photoperiodismService = photoperiodismService;
            _vietnamUnitsService = vietnamUnitsService;
            _productTypesService = productTypesService;
            _householdsService = householdsService;
            _env = env;
        }

        public async Task InitDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateAllExistingRolesAsync();

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
                    UserProfile profile = new()
                    {
                        FirstName = _superAdminDefaultOptions.FirstName,
                        LastName = _superAdminDefaultOptions.LastName,
                        Email = superAdmin.Email,
                        ApplicationUserId = superAdmin.Id.ToString(),
                        Phone = _superAdminDefaultOptions.Phone,
                        Address = _superAdminDefaultOptions.Address,
                        Birthday = DateTime.Now
                    };
                    await _userProfilesService.CreateAsync(profile);
                    await _roles.AssignAllRolesToUserAsync(superAdmin.Id.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InitAppUserData()
        {
            var provinces = new VietnameUnits(_env).GetAllProvincesAsync();
            await _vietnamUnitsService.CreateManyAsync(provinces);
            await new UserProfileSeeder().InitData();
            await new FarmSeeder(_farmTypesService).InitData();
            await new PhotoperiodismSeeder(_photoperiodismService).InitData();
            await new ProductTypeSeeder(_productTypesService).InitData();
            await new FarmingHouseholdSeederTest(_householdsService).InitData();
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

        public async Task<string> UploadFile(IList<IFormFile> files, IWebHostEnvironment env, string uploadFolder)
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
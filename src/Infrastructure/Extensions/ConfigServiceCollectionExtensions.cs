using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace Infrastructure.Extensions
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
            this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection("SupplyManagementDatabase").Get<DbSettings>();
            services.Configure<DbSettings>(configuration.GetSection("SupplyManagementDatabase"));

            // Get Identity Default Options
            var identityDefaultOptionsConfigurationSection = configuration.GetSection("IdentityDefaultOptions");
            services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfigurationSection);

            var identityDefaultOptions = identityDefaultOptionsConfigurationSection.Get<IdentityDefaultOptions>();
            services.AddIdentity<ApplicationUser, ApplicationRole>();
            services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, ObjectId>(new MongoDbIdentityConfiguration()
            {
                MongoDbSettings = new MongoDbSettings
                {
                    DatabaseName = dbSettings.DatabaseName,
                    ConnectionString = dbSettings.ConnectionString
                },
                IdentityOptionsAction = options =>
                {
                    // Password settings
                    options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
                    options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
                    options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
                    options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
                    options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                    options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
                    options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

                    // User settings
                    options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

                    // Email confirmation require
                    options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;
                }
            }).AddDefaultTokenProviders();

            // cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = identityDefaultOptions.CookieHttpOnly;
                options.ExpireTimeSpan = TimeSpan.FromDays(identityDefaultOptions.CookieExpiration);
                options.LoginPath = identityDefaultOptions.LoginPath; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = identityDefaultOptions.LogoutPath; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = identityDefaultOptions.AccessDeniedPath; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = identityDefaultOptions.SlidingExpiration;
            });

            // Get SendGrid configuration options
            services.Configure<SendGridOptions>(configuration.GetSection("SendGridOptions"));

            // Get SMTP configuration options
            services.Configure<SmtpOptions>(configuration.GetSection("SmtpOptions"));

            // Get Super Admin Default options
            services.Configure<SuperAdminDefaultOptions>(configuration.GetSection("SuperAdminDefaultOptions"));

            return services;
        }

        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            // Add email services
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IRoles, Roles>();

            services.AddTransient<IFunctional, Functional>();

            return services;
        }

        public static IServiceCollection AddSingletons(this IServiceCollection services)
        {
            services.AddSingleton<UserProfilesService>();

            services.AddSingleton<UnitOfMeasuresService>();

            services.AddSingleton<ApplicationUsersService>();

            services.AddSingleton<FarmTypesService>();

            services.AddSingleton<FarmsService>();

            services.AddSingleton<PhotoperiodismService>();

            services.AddSingleton<VietnamUnitsService>();

            services.AddSingleton<FarmingHouseholdsService>();

            services.AddSingleton<ProductTypesService>();

            services.AddSingleton<ProductsService>();

            services.AddSingleton<TransportersService>();

            services.AddSingleton<RetailersService>();

            services.AddSingleton<AgriculturalProductsService>();

            return services;
        }
    }
}

using SupplyChainManagement.Services.Database;
using System.Text.Json.Serialization;
using System.Text.Json;
using SupplyChainManagement.Extensions;
using Microsoft.AspNetCore.Identity;
using SupplyChainManagement.Models;
using SupplyChainManagement.Services;
using SupplyChainManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// =======================================================================

builder.Services.AddConfig(builder.Configuration)
    .AddMyDependencyGroup();

builder.Services.AddSingleton<UsersService>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddRazorPages();

// =======================================================================

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var usersService = services.GetRequiredService<UsersService>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var functional = services.GetRequiredService<IFunctional>();

        DbInitializer.Initialize(usersService, functional);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https:// aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
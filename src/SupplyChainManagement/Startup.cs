using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.Services;
using SupplyChainManagement.Services.Database;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupplyChainManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // public void Configure(IApplicationBuilder app)
        // {
        //    // For more information on how to configure your application, visit https:// go.microsoft.com/fwlink/?LinkID=398940
        // }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // / <summary>
        // / This method gets called by the runtime. Use this method to add services to the container.
        // / </summary>
        // / <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=UserRole}/{action=UserProfile}/{id?}");
            });

        }
    }
}

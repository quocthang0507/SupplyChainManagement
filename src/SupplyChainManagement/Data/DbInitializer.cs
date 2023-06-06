using SupplyChainManagement.Services;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(UsersService service, IFunctional functional)
        {
            // check for users
            if ((await service.GetAsync()).Any())
            {
                return; // if user is not empty, DB has been seed
            }

            // init app with super admin user
            await functional.CreateDefaultSuperAdmin();

            // init app data
            await functional.InitAppData();

        }
    }
}

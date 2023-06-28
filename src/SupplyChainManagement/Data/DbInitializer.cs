using SupplyChainManagement.Services;
using SupplyChainManagement.Services.Database;

namespace SupplyChainManagement.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(UserProfilesService usersService, IFunctional functional)
        {
            // check for users
            if ((await usersService.GetAsync()).Any())
            {
                return; // if user is not empty, DB has been seed
            }

            // init app with super admin user
            await functional.InitDefaultSuperAdmin();

            // init app data
            await functional.InitAppUserData();

        }
    }
}

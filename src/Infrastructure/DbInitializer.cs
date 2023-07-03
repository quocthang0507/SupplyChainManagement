using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure
{
    public static class DbInitializer
    {
        public static async Task Initialize(UserProfilesService usersService, IFunctional functional)
        {
            if ((await usersService.GetAsync()).Any())
            {
                return;
            }

            await functional.InitDefaultSuperAdmin();

            await functional.InitAppUserData();

        }
    }
}

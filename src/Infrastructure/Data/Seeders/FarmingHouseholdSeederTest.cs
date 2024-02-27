using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Seeders
{
    public class FarmingHouseholdSeederTest(FarmingHouseholdsService householdsService) : ISeeder
    {
        private readonly FarmingHouseholdsService householdsService = householdsService;

        public async Task InitData()
        {
            await householdsService.CreateAsync(new FarmingHousehold()
            {
                FirstName = "Vũ Thị Kim",
                LastName = "Anh",
                Address = new Address()
                {
                    Street = "Tổ dân phố Bon Đưng II",
                    Commune = "Thị trấn Lạc Dương",
                    District = "Lạc Dương",
                    Province = "Lâm Đồng"
                },
                Phone = "0999999039"
            });
        }
    }
}

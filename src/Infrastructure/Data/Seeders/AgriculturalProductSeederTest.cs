using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Seeders
{
    public class AgriculturalProductSeederTest(AgriculturalProductsService productsService) : ISeeder
    {
        private readonly AgriculturalProductsService productsService = productsService;

        public async Task InitData()
        {
            await productsService.CreateAsync(new AgriculturalProduct()
            {

            });
        }
    }
}

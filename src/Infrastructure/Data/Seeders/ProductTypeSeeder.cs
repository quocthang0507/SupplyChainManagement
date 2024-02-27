using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Seeders
{
    public class ProductTypeSeeder(ProductTypesService productTypesService) : ISeeder
    {
        private readonly ProductTypesService productTypesService = productTypesService;

        public async Task InitData()
        {
            await productTypesService.CreateAsync(new AgriculturalProductType()
            {
                EnglishProductTypeName = "Crop",
                ProductTypeName = "Cây trồng",
                Description = "Crop is a plant or plant product that is grown for a specific purpose such as food, fibre or fuel."
            });
            await productTypesService.CreateAsync(new AgriculturalProductType()
            {
                EnglishProductTypeName = "Livestock",
                ProductTypeName = "Súc sản",
                Description = "Livestock farming is the practice of raising animals for their products."
            });
            await productTypesService.CreateAsync(new AgriculturalProductType()
            {
                EnglishProductTypeName = "Aquaculture",
                ProductTypeName = "Thủy sản",
                Description = "Aquaculture can also be defined as the breeding, growing, and harvesting of fish and other aquatic plants, also known as farming in water."
            });
            await productTypesService.CreateAsync(new AgriculturalProductType()
            {
                EnglishProductTypeName = "Forestry",
                ProductTypeName = "Lâm sản",
                Description = "A forest product is any material derived from forestry for direct consumption or commercial use, such as lumber, paper, or fodder for livestock."
            });
        }
    }
}

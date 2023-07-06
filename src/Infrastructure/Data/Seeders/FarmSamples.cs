using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Seeders
{
    public class FarmSamples : ISeeder
    {
        private readonly FarmTypeService farmTypeService;
        private readonly FarmService farmService;

        public FarmSamples(FarmTypeService farmTypeService, FarmService farmService)
        {
            this.farmTypeService = farmTypeService;
            this.farmService = farmService;
        }

        public async Task InitData()
        {
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Livestock farming",
                FarmTypeName = "Chăn nuôi",
                Description = "Pastoral farming (also known in some regions as ranching, livestock farming or grazing) is aimed at producing livestock, rather than growing crops."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Dairy farming",
                FarmTypeName = "Chăn nuôi lấy sữa",
                Description = "Dairy farming is a class of agriculture for long-term production of milk, which is processed (either on the farm or at a dairy plant, either of which may be called a dairy) for eventual sale of a dairy product."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Crop farming",
                FarmTypeName = "Trồng trọt",
                Description = "Crop farming is the cultivation of plants for food, animal foodstuffs, or other commercial uses."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Aquaculture",
                FarmTypeName = "Nuôi trồng thủy sản",
                Description = "Aquaculture can also be defined as the breeding, growing, and harvesting of fish and other aquatic plants, also known as farming in water."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Orchards",
                FarmTypeName = "Vườn cây ăn trái",
                Description = "Orchards comprise fruit- or nut-producing trees that are generally grown for commercial production."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Organic farming",
                FarmTypeName = "Vườn hữu cơ",
                Description = "Organic farming, also known as ecological farming or biological farming, is an agricultural system that uses fertilizers of organic origin such as compost manure, green manure, and bone meal and places emphasis on techniques such as crop rotation and companion planting."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Hydroponics",
                FarmTypeName = "Thủy canh",
                Description = "Hydroponics is a type of horticulture and a subset of hydroculture which involves growing plants, usually crops or medicinal plants, without soil, by using water-based mineral nutrient solutions."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Agroforestry",
                FarmTypeName = "Nông lâm kết hợp",
                Description = "Agroforestry refers to any of a broad range of land use practices where pasture or crops are integrated with trees and shrubs."
            });
            await farmTypeService.CreateAsync(new FarmType()
            {
                EnglishTypeName = "Mixed farming",
                FarmTypeName = "Vườn ao chuồng",
                Description = "Mixed farming is a type of farming which involves both the growing of crops and the raising of livestock."
            });
        }
    }
}

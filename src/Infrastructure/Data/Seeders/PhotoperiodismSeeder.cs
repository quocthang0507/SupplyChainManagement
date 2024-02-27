using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Seeders
{
    public class PhotoperiodismSeeder(PhotoperiodismService photoperiodismService) : ISeeder
    {
        private readonly PhotoperiodismService photoperiodismService = photoperiodismService;

        public async Task InitData()
        {
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Short Day Plants",
                Name = "Thực vật ngắn ngày"
            });
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Long Day Plants",
                Name = "Thực vật dài ngày"
            });
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Day Neutral Plants",
                Name = "Thực vật ngày trung bình"
            });
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Intermediate Plants",
                Name = "Thực vật trung gian"
            });
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Short–Long Day Plants",
                Name = "Thực vật ngắn-dài ngày"
            });
            await photoperiodismService.CreateAsync(new()
            {
                EnglishName = "Long-Short Day Plants",
                Name = "Thực vật dài-ngắn ngày"
            });
        }
    }
}

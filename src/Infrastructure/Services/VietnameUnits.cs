using ApplicationCore.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Services
{
    public class VietnameUnits
    {
        private readonly IWebHostEnvironment _env;

        public VietnameUnits(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Province> GetAllProvincesAsync()
        {
            string path = Path.Join(_env.ContentRootPath, "Resources", "VietnamUnits.json");
            using StreamReader reader = new(path);
            string json = reader.ReadToEnd();
            var deserializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            return JsonConvert.DeserializeObject<List<Province>>(json, deserializerSettings);
        }
    }
}

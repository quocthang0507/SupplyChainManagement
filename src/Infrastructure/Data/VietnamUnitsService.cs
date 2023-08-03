using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Data
{
    [CollectionName("VietnamUnits")]
    public class VietnamUnitsService : IService<Province>
    {
        private readonly IMongoCollection<Province> _provinceCollection;

        public VietnamUnitsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _provinceCollection = mongoDb.GetCollection<Province>(dbSettings.Value.VietnamUnitsCollectionName);
        }

        public async Task CreateAsync(Province province) =>
            await _provinceCollection.InsertOneAsync(province);

        public async Task CreateManyAsync(List<Province> provinces) =>
            await _provinceCollection.InsertManyAsync(provinces);

        public async Task<List<string>> GetProvinces() =>
            (await _provinceCollection.Find(_ => true).ToListAsync()).Select(p => p.Name).ToList();

        public async Task<List<string>> GetDistricts(string province)
        {
            var provinceInfo = await _provinceCollection.Find(p => p.Name == province).FirstOrDefaultAsync();
            if (provinceInfo != null)
                return provinceInfo.Districts.Select(d => d.Name).ToList();
            return new List<string>();
        }

        public async Task<List<string>> GetWards(string province, string district)
        {
            var provinceInfo = await _provinceCollection.Find(p => p.Name == province).FirstOrDefaultAsync();
            if (provinceInfo != null)
            {
                var districts = provinceInfo.Districts;
                var districtsInfo = districts.Find(d => d.Name == district);
                return districtsInfo != null ? districtsInfo.Wards.Select(w => w.Name).ToList() : new List<string>();
            }
            return new List<string>();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Province>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Province?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, Province t)
        {
            throw new NotImplementedException();
        }
    }
}

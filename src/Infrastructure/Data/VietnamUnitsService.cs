using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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

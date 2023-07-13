using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class FarmsService : IService<Farm>
    {
        private readonly IMongoCollection<Farm> _farmCollection;

        public FarmsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _farmCollection = mongoDb.GetCollection<Farm>(dbSettings.Value.FarmsCollectionName);
        }

        public async Task CreateAsync(Farm farm) =>
            await _farmCollection.InsertOneAsync(farm);

        public async Task DeleteAsync(string id) =>
            await _farmCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Farm>> GetAsync() =>
            await _farmCollection.Find(_ => true).ToListAsync();

        public async Task<Farm?> GetAsync(string id) =>
            await _farmCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Farm farm) =>
            await _farmCollection.ReplaceOneAsync(x => x.Id == id, farm);

    }
}

using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class FarmTypeService : IService<FarmType>
    {
        private readonly IMongoCollection<FarmType> _farmTypeCollection;

        public FarmTypeService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _farmTypeCollection = mongoDb.GetCollection<FarmType>(dbSettings.Value.FarmTypeCollectionName);
        }

        public async Task CreateAsync(FarmType farmType) =>
            await _farmTypeCollection.InsertOneAsync(farmType);

        public async Task DeleteAsync(string id) =>
            await _farmTypeCollection.DeleteOneAsync(x => x.FarmTypeId == id);

        public async Task<List<FarmType>> GetAsync() =>
            await _farmTypeCollection.Find(_ => true).ToListAsync();

        public async Task<FarmType?> GetAsync(string id) =>
            await _farmTypeCollection.Find(x => x.FarmTypeId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, FarmType farmType) =>
            await _farmTypeCollection.ReplaceOneAsync(x => x.FarmTypeId == id, farmType);
    }
}

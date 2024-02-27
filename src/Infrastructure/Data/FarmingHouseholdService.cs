using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class FarmingHouseholdService : IService<FarmingHousehold>
    {
        private readonly IMongoCollection<FarmingHousehold> _farmingHouseholdsCollection;

        public FarmingHouseholdService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _farmingHouseholdsCollection = mongoDb.GetCollection<FarmingHousehold>(dbSettings.Value.FarmingHouseholdCollectionName);
        }

        public async Task<List<FarmingHousehold>> GetAsync() =>
            await _farmingHouseholdsCollection.Find(_ => true).ToListAsync();

        public async Task<IPagedList<FarmingHousehold>> GetPagedAsync(IPagingParams pagingParams) =>
            await _farmingHouseholdsCollection.Find(_ => true).ToPagedListAsync(pagingParams);

        public async Task<FarmingHousehold?> GetAsync(string id) =>
            await _farmingHouseholdsCollection.Find(x => x.UserProfileId == id).FirstOrDefaultAsync();

        public async Task<FarmingHousehold?> GetByApplicationUserIdAsync(string id) =>
            await _farmingHouseholdsCollection.Find(x => x.ApplicationUserId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(FarmingHousehold farmingHousehold) =>
            await _farmingHouseholdsCollection.InsertOneAsync(farmingHousehold);

        public async Task UpdateAsync(string id, FarmingHousehold farmingHousehold) =>
            await _farmingHouseholdsCollection.ReplaceOneAsync(x => x.UserProfileId == id, farmingHousehold);

        public async Task SetInactivatedAsync(string id) =>
            await _farmingHouseholdsCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<FarmingHousehold>.Update.Set(x => x.Activated, false));

        public async Task SetActivatedAsync(string id) =>
            await _farmingHouseholdsCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<FarmingHousehold>.Update.Set(x => x.Activated, true));

        public async Task DeleteAsync(string id) =>
            await _farmingHouseholdsCollection.DeleteOneAsync(x => x.UserProfileId == id);
    }
}

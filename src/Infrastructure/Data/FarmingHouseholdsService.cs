using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class FarmingHouseholdsService : IService<FarmingHousehold>
    {
        private readonly IMongoCollection<FarmingHousehold> _householdCollection;

        public FarmingHouseholdsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _householdCollection = mongoDb.GetCollection<FarmingHousehold>(dbSettings.Value.FarmingHouseholdsCollectionName);
        }

        public async Task<List<FarmingHousehold>> GetAsync() =>
            await _householdCollection.Find(_ => true).ToListAsync();

        public async Task<IPagedList<FarmingHousehold>> GetPagedAsync(IPagingParams pagingParams) =>
            await _householdCollection.Find(_ => true).ToPagedListAsync(pagingParams);

        public async Task<FarmingHousehold?> GetAsync(string id) =>
            await _householdCollection.Find(x => x.UserProfileId == id).FirstOrDefaultAsync();

        public async Task<FarmingHousehold?> GetByApplicationUserIdAsync(string id) =>
            await _householdCollection.Find(x => x.ApplicationUserId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(FarmingHousehold user) =>
            await _householdCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, FarmingHousehold user) =>
            await _householdCollection.ReplaceOneAsync(x => x.UserProfileId == id, user);

        public async Task SetInactivatedAsync(string id) =>
            await _householdCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<FarmingHousehold>.Update.Set(x => x.Activated, false));

        public async Task SetActivatedAsync(string id) =>
            await _householdCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<FarmingHousehold>.Update.Set(x => x.Activated, true));

        public async Task DeleteAsync(string id) =>
            await _householdCollection.DeleteOneAsync(x => x.UserProfileId == id);

        public async Task<bool> ExistsAsync(string userProfileId, string applicationUserId)
        {
            FarmingHousehold? profile = await GetAsync(userProfileId);
            if (profile != null)
                return profile.ApplicationUserId == applicationUserId;
            return false;
        }
    }
}

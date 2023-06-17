using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Services.Database
{
    public class UserProfilesService : IService<UserProfile>
    {
        private readonly IMongoCollection<UserProfile> _usersCollection;

        public UserProfilesService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDb.GetCollection<UserProfile>(dbSettings.Value.UserProfilesCollectionName);
        }

        public async Task<List<UserProfile>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<UserProfile?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.UserProfileId == id).FirstOrDefaultAsync();

        public async Task<UserProfile?> GetByApplicationUserIdAsync(string id) =>
            await _usersCollection.Find(x => x.ApplicationUserId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserProfile user) =>
            await _usersCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, UserProfile user) =>
            await _usersCollection.ReplaceOneAsync(x => x.UserProfileId == id, user);

        public async Task SetInactivatedAsync(string id) =>
            await _usersCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<UserProfile>.Update.Set(x => x.Activated, false));

        public async Task SetActivatedAsync(string id) =>
            await _usersCollection.FindOneAndUpdateAsync(
                x => x.UserProfileId == id,
                Builders<UserProfile>.Update.Set(x => x.Activated, true));
    }
}

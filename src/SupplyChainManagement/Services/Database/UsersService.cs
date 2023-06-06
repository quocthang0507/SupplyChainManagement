using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Services.Database
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDb.GetCollection<User>(dbSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _usersCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User user) =>
            await _usersCollection.ReplaceOneAsync(x => x.UserId == id, user);

        public async Task SetInactivatedAsync(string id) =>
            await _usersCollection.FindOneAndUpdateAsync(
                x => x.UserId == id,
                Builders<User>.Update.Set(x => x.Activated, false));

        public async Task SetActivatedAsync(string id) =>
            await _usersCollection.FindOneAndUpdateAsync(
                x => x.UserId == id,
                Builders<User>.Update.Set(x => x.Activated, true));
    }
}

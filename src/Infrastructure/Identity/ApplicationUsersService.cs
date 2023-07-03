using ApplicationCore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Identity
{
    public class ApplicationUsersService
    {
        private readonly IMongoCollection<ApplicationUser> _applicationUsersCollection;

        public ApplicationUsersService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _applicationUsersCollection = mongoDb.GetCollection<ApplicationUser>(dbSettings.Value.ApplicationUsersCollectionName);
        }

        public async Task<List<ApplicationUser>> GetAsync() =>
            await _applicationUsersCollection.Find(_ => true).ToListAsync();

        public async Task<ApplicationUser?> GetAsync(string id) =>
            await _applicationUsersCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();

    }
}

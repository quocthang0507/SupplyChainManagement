using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class RetailersService : IService<Retailer>
    {
        private readonly IMongoCollection<Retailer> _transporterCollection;

        public RetailersService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _transporterCollection = mongoDb.GetCollection<Retailer>(dbSettings.Value.RetailersCollectionName);
        }

        public async Task CreateAsync(Retailer retailer) =>
            await _transporterCollection.InsertOneAsync(retailer);

        public async Task DeleteAsync(string id) =>
            await _transporterCollection.DeleteOneAsync(x => x.RetailerId == id);

        public async Task<List<Retailer>> GetAsync() =>
            await _transporterCollection.Find(_ => true).ToListAsync();

        public async Task<Retailer?> GetAsync(string id) =>
            await _transporterCollection.Find(x => x.RetailerId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Retailer retailer) =>
            await _transporterCollection.ReplaceOneAsync(x => x.RetailerId == id, retailer);

    }
}

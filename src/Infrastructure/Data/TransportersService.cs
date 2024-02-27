using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class TransportersService : IService<Transporter>
    {
        private readonly IMongoCollection<Transporter> _transporterCollection;

        public TransportersService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _transporterCollection = mongoDb.GetCollection<Transporter>(dbSettings.Value.TransportersCollectionName);
        }

        public async Task CreateAsync(Transporter transporter) =>
            await _transporterCollection.InsertOneAsync(transporter);

        public async Task DeleteAsync(string id) =>
            await _transporterCollection.DeleteOneAsync(x => x.TransporterId == id);

        public async Task<List<Transporter>> GetAsync() =>
            await _transporterCollection.Find(_ => true).ToListAsync();

        public async Task<Transporter?> GetAsync(string id) =>
            await _transporterCollection.Find(x => x.TransporterId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Transporter transporter) =>
            await _transporterCollection.ReplaceOneAsync(x => x.TransporterId == id, transporter);
    }
}

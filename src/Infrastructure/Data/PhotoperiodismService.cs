using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static ApplicationCore.Entities.Taxonomy;

namespace Infrastructure.Data
{
    public class PhotoperiodismService : IService<Photoperiodism>
    {
        private readonly IMongoCollection<Photoperiodism> _photoperiodismCollection;

        public PhotoperiodismService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _photoperiodismCollection = mongoDb.GetCollection<Photoperiodism>(dbSettings.Value.PhotoperiodismCollectionName);
        }

        public async Task CreateAsync(Photoperiodism photoperiodism) =>
            await _photoperiodismCollection.InsertOneAsync(photoperiodism);

        public async Task DeleteAsync(string id) =>
            await _photoperiodismCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Photoperiodism>> GetAsync() =>
            await _photoperiodismCollection.Find(_ => true).ToListAsync();

        public async Task<Photoperiodism?> GetAsync(string id) =>
            await _photoperiodismCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Photoperiodism t) =>
            await _photoperiodismCollection.ReplaceOneAsync(x => x.Id == id, t);
    }
}

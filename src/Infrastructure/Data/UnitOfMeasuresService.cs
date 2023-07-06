using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class UnitOfMeasuresService : IService<UnitOfMeasure>
    {
        private readonly IMongoCollection<UnitOfMeasure> _unitOfMeasureCollection;

        public UnitOfMeasuresService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _unitOfMeasureCollection = mongoDb.GetCollection<UnitOfMeasure>(dbSettings.Value.UnitOfMeasuresCollectionName);
        }

        public async Task CreateAsync(UnitOfMeasure unitOfMeasure) =>
            await _unitOfMeasureCollection.InsertOneAsync(unitOfMeasure);

        public async Task DeleteAsync(string id) =>
            await _unitOfMeasureCollection.DeleteOneAsync(x => x.UnitOfMeasureId == id);

        public async Task<List<UnitOfMeasure>> GetAsync() =>
            await _unitOfMeasureCollection.Find(_ => true).ToListAsync();

        public async Task<UnitOfMeasure?> GetAsync(string id) =>
            await _unitOfMeasureCollection.Find(x => x.UnitOfMeasureId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, UnitOfMeasure unitOfMeasure) =>
            await _unitOfMeasureCollection.ReplaceOneAsync(x => x.UnitOfMeasureId == id, unitOfMeasure);
    }
}

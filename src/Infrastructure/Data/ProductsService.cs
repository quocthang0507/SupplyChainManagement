using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class ProductsService : IService<AgriculturalProduct>
    {
        private readonly IMongoCollection<AgriculturalProduct> _productCollection;

        public ProductsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _productCollection = mongoDb.GetCollection<AgriculturalProduct>(dbSettings.Value.ProductsCollectionName);
        }

        public async Task CreateAsync(AgriculturalProduct product) =>
            await _productCollection.InsertOneAsync(product);

        public async Task DeleteAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);

        public async Task<List<AgriculturalProduct>> GetAsync() =>
            await _productCollection.Find(_ => true).ToListAsync();

        public async Task<AgriculturalProduct?> GetAsync(string id) =>
            await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, AgriculturalProduct product) =>
            await _productCollection.ReplaceOneAsync(x => x.ProductId == id, product);
    }

}
}

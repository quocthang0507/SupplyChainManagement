using ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class ProductTypesService : IService<AgriculturalProductType>
    {
        private readonly IMongoCollection<AgriculturalProductType> _productTypeCollection;

        public ProductTypesService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _productTypeCollection = mongoDb.GetCollection<AgriculturalProductType>(dbSettings.Value.ProductTypesCollectionName);
        }

        public async Task CreateAsync(AgriculturalProductType productType) =>
            await _productTypeCollection.InsertOneAsync(productType);

        public async Task DeleteAsync(string id) =>
            await _productTypeCollection.DeleteOneAsync(x => x.ProductTypeId == id);

        public async Task<List<AgriculturalProductType>> GetAsync() =>
            await _productTypeCollection.Find(_ => true).ToListAsync();

        public async Task<AgriculturalProductType?> GetAsync(string id) =>
            await _productTypeCollection.Find(x => x.ProductTypeId == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, AgriculturalProductType productType) =>
            await _productTypeCollection.ReplaceOneAsync(x => x.ProductTypeId == id, productType);
    }
}

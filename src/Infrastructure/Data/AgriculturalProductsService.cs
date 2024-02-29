using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class AgriculturalProductsService : IService<AgriculturalProduct>
    {
        private readonly IMongoCollection<AgriculturalProduct> _productCollection;

        public AgriculturalProductsService(IOptions<DbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _productCollection = mongoDb.GetCollection<AgriculturalProduct>(dbSettings.Value.AgriculturalProductsCollectionName);
        }

        public async Task<List<AgriculturalProduct>> GetAsync() =>
            await _productCollection.Find(_ => true).ToListAsync();

        public async Task<IPagedList<AgriculturalProduct>> GetPagedAsync(IPagingParams pagingParams) =>
            await _productCollection.Find(_ => true).ToPagedListAsync(pagingParams);

        public async Task<AgriculturalProduct?> GetAsync(string id) =>
            await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(AgriculturalProduct user) =>
            await _productCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, AgriculturalProduct user) =>
            await _productCollection.ReplaceOneAsync(x => x.ProductId == id, user);

        public async Task DeleteAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
    }
}

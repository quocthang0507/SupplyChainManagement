using MongoDB.Driver;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Services.Database
{
    public interface IService<T>
    {
        public Task<List<T>> GetAsync();
        public Task<T?> GetAsync(string id);
        public Task CreateAsync(T t);
        public Task UpdateAsync(string id, T t);
        public Task DeleteAsync(string id);
    }
}

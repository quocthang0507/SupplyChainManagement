using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace SupplyChainManagement.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<ObjectId>
    {

    }
}

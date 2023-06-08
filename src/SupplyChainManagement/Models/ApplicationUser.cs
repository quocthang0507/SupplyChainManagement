using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace SupplyChainManagement.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<ObjectId>
    {
    }
}

using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Identity
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<ObjectId>
    {
    }
}

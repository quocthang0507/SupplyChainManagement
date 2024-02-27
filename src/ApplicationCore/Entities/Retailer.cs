using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nhà bán lẻ
    /// </summary>
    [CollectionName("Retailers")]
    public class Retailer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RetailerId { get; set; }
    }
}

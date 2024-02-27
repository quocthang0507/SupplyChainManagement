using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nhà vận chuyển
    /// </summary>
    [CollectionName("Transporters")]
    public class Transporter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransporterId { get; set; }
    }
}

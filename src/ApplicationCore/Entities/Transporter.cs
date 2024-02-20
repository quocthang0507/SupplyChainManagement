using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nhà vận chuyển
    /// </summary>
    public class Transporter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransporterId { get; set; }
    }
}

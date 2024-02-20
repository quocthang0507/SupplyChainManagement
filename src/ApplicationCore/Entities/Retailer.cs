using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Nhà bán lẻ
    /// </summary>
    public class Retailer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RetailerId { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    [CollectionName("UnitOfMeasures")]
    public class UnitOfMeasure
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UnitOfMeasureId { get; set; }
        [Required]
        public string UnitOfMeasureName { get; set; } = null!;
        public string? Description { get; set; }
    }
}

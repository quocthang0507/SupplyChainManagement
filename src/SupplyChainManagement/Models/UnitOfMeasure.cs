using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDbGenericRepository.Attributes;

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
